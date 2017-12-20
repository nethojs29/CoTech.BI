using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Controllers;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Events;
using CoTech.Bi.Modules.Wer.Models.Notifications;
using CoTech.Bi.Modules.Wer.Models.Responses;
using CoTech.Bi.Modules.Wer.Repositories;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Notifiers
{
    public class ReportNotifier
    {
        public ReportNotifier(){

            var eventObs = DbObservable<BiContext>.FromInserted<EventEntity>();
            eventObs.Where(evt => evt.Entity.Body is ReportUpdatedEvt).Subscribe(OnUpdated);
        }

        private void OnUpdated(IAfterEntry<EventEntity, BiContext> evt)
        {
            var _notification = new NotificationsIOSRepository(evt.Context);
            var _dbNotification = evt.Context.Set<NotificationEntity>();
            var _dbReport = evt.Context.Set<ReportEntity>();
            var reportEvt = evt.Entity.Body as ReportUpdatedEvt;
            var report = _dbReport.Include(r => r.Company).Include(r => r.Week).Include(r => r.User).First(r => r.Id == reportEvt.Id);
            var users = this.UserFromReport(evt.Context, report);
            _dbNotification.Add(new NotificationEntity()
            {
                Body = new ReportUpdated()
                {
                    Id = report.Id,
                    CompanyId = report.CompanyId,
                    UserId = report.UserId,
                    WeekId = report.WeekId
                },
                SenderId = evt.Entity.UserId,
                Receivers = users.Select(u => new ReceiverEntity()
                {
                    UserId = u
                }).ToList()
            });
            var message = "Se actualizó reporte de la empresa: " + report.Company.Name + " por el usuario: " +
                          report.User.Name + " " + report.User.Lastname + " en la semana: " +
                          report.Week.StartTime.ToString("dd MMM").ToUpper() + " al " +
                          report.Week.EndTime.ToString("dd MMM yyyy").ToUpper();
            _notification.SendNotification(users,message, report);
            evt.Context.SaveChanges();
        }

        private List<long> UserFromReport(BiContext context, ReportEntity report)
        {
            var _dbCompany = context.Set<CompanyEntity>();
            var _dbUser = context.Set<PermissionEntity>();
            var userList = new List<long>();
            var company = _dbCompany.Find(report.CompanyId);
            userList.Add(report.UserId);
            while(company.ParentId != null)
            {
                company = _dbCompany.Find(company.ParentId);
            }
            var ceo = _dbUser.Where(p => p.CompanyId == company.Id && (p.RoleId == 602 || p.RoleId == 603))
                .Select(p => p.UserId).ToList();
            return userList.Concat(ceo).ToList();
        }

    }
}