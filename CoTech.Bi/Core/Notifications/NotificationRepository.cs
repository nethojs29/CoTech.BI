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

        public Task<List<NotificationEntity>> AllForUser(long userId) {
            return db.Where(n => n.Receivers.Any(r => r.UserId == userId))
                .Include(n => n.Receivers)
                .ToListAsync();
        }

        public IObservable<NotificationEntity> Listen(long userId) {
            return DbObservable<BiContext>.FromInserted<NotificationEntity>()
                .Where(n => n.Entity.Receivers.Any(u => u.UserId == userId))
                .Select(n => n.Entity);
        }

        public async Task Create(NotificationEntity entity){
          db.Add(entity);
          await context.SaveChangesAsync();
        } 

        public async Task<Boolean> MarkAsRead(long id, long userId) {
            var notification = await db.Include(n => n.Receivers)
                .FirstAsync(n => n.Id == id);
            var receiver = notification.Receivers.First(r => r.UserId == userId);
            if (receiver == null) {
                return false;
            }
            receiver.Read = true;
            db.Update(notification);
            await context.SaveChangesAsync();
            return true;
        }
    }
}