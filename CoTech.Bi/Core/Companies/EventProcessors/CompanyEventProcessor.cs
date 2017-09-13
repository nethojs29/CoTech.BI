using System;
using System.Reactive.Linq;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Companies.EventProcessors
{
    public class CompanyEventProcessor {
        private IObservable<IBeforeEntry<EventEntity, BiContext>> eventObservable;
        public CompanyEventProcessor()
        {
            eventObservable = DbObservable<BiContext>.FromInserting<EventEntity>()
                .Where(entry => entry.Entity.Body is CompanyEvent);
            eventObservable.Where(entry => 
                entry.Entity.Body is CompanyCreatedEvt
            ).Subscribe(onCreate);
        }

        private DbSet<CompanyEntity> getDb(IBeforeEntry<EventEntity, BiContext> entry){
            return entry.Context.Set<CompanyEntity>();
        }

        private void onCreate(IBeforeEntry<EventEntity, BiContext> entry) {
            Console.Write(entry);
            var db = getDb(entry);
            var eventBody = entry.Entity.Body as CompanyCreatedEvt;
            var companyEntity = new CompanyEntity {
                Name = eventBody.Name,
                Activity = eventBody.Activity,
                Url = eventBody.Url
            };
            db.Add(companyEntity);
            entry.Context.SaveChanges();
        }

        private void onUpdate(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = getDb(entry);
            var eventBody = entry.Entity.Body as CompanyUpdatedEvt;
            var companyEntity = db.Find(eventBody.Id);
            if(eventBody.Name != null) companyEntity.Name = eventBody.Name;
            if(eventBody.Activity != null) companyEntity.Activity = eventBody.Activity;
            if(eventBody.Url != null) companyEntity.Url = eventBody.Url;
            db.Update(companyEntity);
            entry.Context.SaveChanges();
        }
    }
}