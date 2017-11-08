using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Responses;
using CoTech.Bi.Util;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class NotificationsIOSRepository
    {
        private BiContext _context;

        private DbSet<IOSTokenEntity> _dbToken
        {
            get { return this._context.Set<IOSTokenEntity>(); }
        }
        public NotificationsIOSRepository(BiContext biContext)
        {
            this._context = biContext;
        }

        public IOSTokenEntity Create(long idUser,string token)
        {
            if (_dbToken.Any(t => t.Token == token))
            {
                var tokenEntity = _dbToken.First(t => t.Token == token);
                _dbToken.Remove(tokenEntity);
                var tkn = new IOSTokenEntity()
                {
                    UserId = idUser,
                    Token = token
                };
                _dbToken.Add(tkn);
                _context.SaveChanges();
                return tkn;
            }
            else
            {
                var tkn = new IOSTokenEntity()
                {
                    UserId = idUser,
                    Token = token
                };
                _dbToken.Add(tkn);
                _context.SaveChanges();
                return tkn;
            }
            return null;
        }

        public void SendNotification(List<long> userIdsList,string message, object data)
        {
            try
            {
                var cert = Directory.GetCurrentDirectory() + "/wwwroot/Certs/RESPUSH .p12";
                var MY_DEVICE_TOKENS = this._dbToken.Where(t => userIdsList.Any(usr => usr == t.UserId)).ToList();
                // Configuration (NOTE: .pfx can also be used here)
                var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,cert, "soy codigo");
                    // Create a new broker
                    var apnsBroker = new ApnsServiceBroker (config);
                    // Wire up events
                    apnsBroker.OnNotificationFailed += (notification, aggregateEx) => {
                        aggregateEx.Handle (ex => {
                            // See what kind of exception it was to further diagnose
                            if (ex is ApnsNotificationException) {
                                var notificationException = (ApnsNotificationException)ex;
                                // Deal with the failed notification
                                var apnsNotification = notificationException.Notification;
                                var statusCode = notificationException.ErrorStatusCode;
                                Console.WriteLine ($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                            } else {
                                // Inner exception might hold more useful information like an ApnsConnectionException			
                                Console.WriteLine ($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                            }
                            // Mark it as handled
                            return true;
                        });
                    };
                    apnsBroker.OnNotificationSucceeded += (notification) => {
                        Console.WriteLine ("Apple Notification Sent!");
                    };
                    // Start the broker
                    apnsBroker.Start ();
                    foreach (var deviceToken in MY_DEVICE_TOKENS) {
                        // Queue a notification to send
                        if (data.GetType() == typeof(ReportEntity))
                        {
                            var report = (ReportEntity) data;
                            apnsBroker.QueueNotification(new ApnsNotification
                            {
                                DeviceToken = deviceToken.Token,
                                Payload = JObject.FromObject(
                                    new
                                    {
                                        aps = new
                                        {
                                            alert = new Dictionary<string, object>()
                                            {
                                                {"title", "Reporte Actualizado"},
                                                {"body",message},
                                                {
                                                    "loc-key", new string[]
                                                    {
                                                        report.CompanyId.ToString(),
                                                        report.UserId.ToString(),
                                                        report.WeekId.ToString()
                                                    }
                                                }
                                            },
                                            sound = "default"
                                        }
                                    }
                                )
                            });
                        }
                        else if(data.GetType() == typeof(MessageResponse))
                        {
                            var messageData = (MessageResponse) data;
                            apnsBroker.QueueNotification(new ApnsNotification
                            {
                                DeviceToken = deviceToken.Token,
                                Payload = JObject.FromObject(
                                    new
                                    {
                                        aps = new
                                        {
                                            alert = new Dictionary<string, object>()
                                            {
                                                {"title", "Nuevo mensaje"},
                                                {"body",message},
                                                {
                                                    "loc-key", new string[]
                                                    {
                                                        messageData.Id.ToString(),
                                                        messageData.GroupId.ToString()
                                                    }
                                                }
                                            },
                                            sound = "default"
                                        }
                                    }
                                )
                            });
                        }
                        else
                        {
                            apnsBroker.QueueNotification (new ApnsNotification {
                                DeviceToken = deviceToken.Token,
                                Payload = JObject.FromObject(
                                    new {aps=new
                                    {
                                        alert = new
                                        {
                                            title = "Aviso:",
                                            body = message
                                        },
                                        sound = "default"
                                    }}
                                )
                            });
                        }
                    }
                    // Stop the broker, wait for it to finish   
                    // This isn't done after every message, but after you're
                    // done with the broker
                    apnsBroker.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}