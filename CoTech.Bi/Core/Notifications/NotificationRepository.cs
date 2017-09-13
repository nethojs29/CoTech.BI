using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Notifications.Repositories
{
    public class NotificationRepository
    {
        private BiContext context;
        private DbSet<NotificationEntity> db {
          get { return context.Set<NotificationEntity>(); }
        }
        private DbSet<ReceiverEntity> dbReceivers {
          get { return context.Set<ReceiverEntity>(); }
        }
        public NotificationRepository(BiContext context){
            this.context = context;
        }

        public IObservable<NotificationEntity> UserNotifications(Guid userId) {
            
            var obs = Observable.Create<NotificationEntity>(async observable => {
                var myNotifs = await db.Where(n => 
                    n.Receivers.Any(u => u.UserId == userId)
                ).ToListAsync();
                myNotifs.ForEach(n => observable.OnNext(n));
                observable.OnCompleted();
            });

            var obs2 = DbObservable<BiContext>.FromInserted<NotificationEntity>()
                .Where(n => n.Entity.Receivers.Any(u => u.UserId == userId))
                .Select(n => n.Entity);
            return obs.Concat(obs2);
        }

        public async Task Create(NotificationEntity entity){
          db.Add(entity);
          await context.SaveChangesAsync();
        } 
    }
}