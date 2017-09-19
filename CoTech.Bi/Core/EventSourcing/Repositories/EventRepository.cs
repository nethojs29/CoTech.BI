using System;
using System.Threading.Tasks;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.EventSourcing.Repositories
{
    public class EventRepository {
        private readonly BiContext context;
        private DbSet<EventEntity> db {
          get { return context.Set<EventEntity>(); }
        }
        public IObservable<IBeforeEntry<EventEntity, BiContext>> evtObs { get; private set;}

        public EventRepository(BiContext context) {
            this.context = context;
            this.evtObs = DbObservable<BiContext>.FromInserting<EventEntity>();
        }

        public async Task<int> Create(EventEntity entity) {
            db.Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<EventEntity> Create(long userId, object body) {
            var entity = new EventEntity{UserId = userId, Body = body};
            await Create(entity);
            return entity;
        }
    }
}