using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Core.Notifications.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Users.Repositories;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;

namespace CoTech.Bi.Core.Companies.Notifiers
{
    public class CompanyNotifier {
        private IObservable<IBeforeEntry<EventEntity, BiContext>> eventObservable;
        public CompanyNotifier(){

            eventObservable = DbObservable<BiContext>.FromInserting<EventEntity>()
                .Where(entry => entry.Entity.Body is CompanyEvent);
            eventObservable.Where(entry => entry.Entity.Body is CompanyUpdatedEvt)
                .Subscribe(onUpdated);
        }

        private void onUpdated(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<NotificationEntity>();
            var dbPermission = entry.Context.Set<PermissionEntity>();
            var eventBody = entry.Entity.Body as CompanyUpdatedEvt;
            var receivers = dbPermission.Where(p => p.CompanyId == eventBody.Id)
                .Select(p => p.UserId).Distinct().ToList();
            var companyEntity = new NotificationEntity {
               SenderId = entry.Entity.UserId,
               Body = eventBody,
               Receivers = receivers.Select(uid => new ReceiverEntity { UserId = uid}).ToList()
            };
            db.Add(companyEntity);
            entry.Context.SaveChanges();
        }
    }
}