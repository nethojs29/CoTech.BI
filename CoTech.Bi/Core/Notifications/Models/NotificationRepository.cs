using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Notifications.Models
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

        public IObservable<NotificationEntity> UserNotifications(long userId) {
            return DbObservable<BiContext>.FromInserted<NotificationEntity>()
                .Where(n => n.Entity.Receivers.Any(u => u.Id == userId))
                .Select(n => n.Entity);
        }

        public async Task Create(NotificationEntity entity){
          db.Add(entity);
          await context.SaveChangesAsync();
        } 
    }
}