using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Util;
using EntityFrameworkCore.Triggers;
using Newtonsoft.Json;

namespace CoTech.Bi.Core.Notifications.Models
{
    public class NotificationEntity : Trackable
    {
        static NotificationEntity() {
          Triggers<NotificationEntity>.Inserted += entry => Console.WriteLine(entry.Entity);
        }
        public long Id { get; set; }
        public long SenderId { get; set; }
        public UserEntity Sender { get; set; }
        public string Type { get; set; }
        public List<ReceiverEntity> Receivers { get; set; }
        [NotMapped]
        public object Body { get; set; }
        [Column("Body")]
        private string BodyJson {
          get { return JsonConvert.SerializeObject(Body, JsonConverterOptions.JsonSettings); }
          set {
            Body = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject(value, JsonConverterOptions.JsonSettings);
          }
        }
    }

    /// <summary>
    /// Tabla de muchos a muchos User-Notification solo para receptores, no emisores
    /// </summary>
    public class ReceiverEntity {
      public long Id { get; set; }
      public long NotificationId { get; set; }
      public long UserId { get; set; }
      public NotificationEntity Notification { get; set; }
      public UserEntity User { get; set; }
      public bool Read { get; set; }
    }
}