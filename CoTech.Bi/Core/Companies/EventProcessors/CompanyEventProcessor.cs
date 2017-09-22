using System;
using System.Linq;
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
            eventObservable = DbObservable<BiContext>
                .FromInserting<EventEntity>()
                .Where(entry => entry.Entity.Body is CompanyEvent);
            eventObservable
                .Where(entry => entry.Entity.Body is CompanyCreatedEvt)
                .Subscribe(onCreate);
            eventObservable
                .Where(entry => entry.Entity.Body is CompanyUpdatedEvt)
                .Subscribe(onUpdate);
            eventObservable
                .Where(entry => entry.Entity.Body is CompanyDeletedEvt)
                .Subscribe(onDelete);
        }

        private void onCreate(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<CompanyEntity>();
            var eventBody = entry.Entity.Body as CompanyCreatedEvt;
            var companyEntity = new CompanyEntity {
                CreatorEventId = entry.Entity.Id,
                Name = eventBody.Name,
                Activity = eventBody.Activity,
                Url = eventBody.Url
            };
            db.Add(companyEntity);
        }

        private void onUpdate(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<CompanyEntity>();
            var eventBody = entry.Entity.Body as CompanyUpdatedEvt;
            var companyEntity = db.Find(eventBody.Id);
            if(eventBody.Name != null) companyEntity.Name = eventBody.Name;
            if(eventBody.Activity != null) companyEntity.Activity = eventBody.Activity;
            if(eventBody.Url != null) {
                if(db.Where(c => c.Url == eventBody.Url).Count() != 0) {
                    entry.Cancel = true;
                    return;
                }
                companyEntity.Url = eventBody.Url;
            }
            db.Update(companyEntity);
        }

        private void onDelete(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<CompanyEntity>();
            var eventBody = entry.Entity.Body as CompanyDeletedEvt;
            var companyEntity = db.Find(eventBody.Id);
            if(companyEntity == null) {
                entry.Cancel = true;
                return;
            }
            companyEntity.DeletedAt = new DateTime();
            db.Update(companyEntity);
        }
    }
}