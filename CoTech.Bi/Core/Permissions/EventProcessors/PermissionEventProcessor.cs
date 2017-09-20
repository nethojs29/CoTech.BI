using System;
using System.Linq;
using System.Reactive.Linq;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Rx;
using EntityFrameworkCore.Triggers;

namespace CoTech.Bi.Core.Permissions.EventProcessors
{
    public class PermissionEventProcessor
    {
        private IObservable<IBeforeEntry<EventEntity, BiContext>> eventObservable;
        public PermissionEventProcessor()
        {
            eventObservable = DbObservable<BiContext>
              .FromInserting<EventEntity>()
              .Where(entry => entry.Entity.Body is PermissionEvent);
            eventObservable
              .Where(entry => entry.Entity.Body is PermissionGivenEvt)
              .Subscribe(onGiven);
            eventObservable
              .Where(entry => entry.Entity.Body is RoleRemovedEvt)
              .Subscribe(onRoleRemoved);
            eventObservable
              .Where(entry => entry.Entity.Body is PermissionsRevokedEvt)
              .Subscribe(onRevoked);
        }

        private void onGiven(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<PermissionEntity>();
            var eventBody = entry.Entity.Body as PermissionGivenEvt;
            var permissionEntity = new PermissionEntity {
              CreatorEventId = entry.Entity.Id,
              CompanyId = eventBody.CompanyId,
              UserId = eventBody.UserId,
              RoleId = eventBody.RoleId
            };
            db.Add(permissionEntity);
        }

        private void onRoleRemoved(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<PermissionEntity>();
            var eventBody = entry.Entity.Body as RoleRemovedEvt;
            var permissionEntity = db.First(p => 
                p.CompanyId == eventBody.CompanyId &&
                p.UserId == eventBody.UserId &&
                p.RoleId == eventBody.RoleId
            );
            if(permissionEntity == null) {
              entry.Cancel = true;
            } else {
              db.Remove(permissionEntity);
            }
        }

        private void onRevoked(IBeforeEntry<EventEntity, BiContext> entry) {
            var db = entry.Context.Set<PermissionEntity>();
            var eventBody = entry.Entity.Body as PermissionsRevokedEvt;
            var permissionEntities = db.Where(p => 
                p.CompanyId == eventBody.CompanyId &&
                p.UserId == eventBody.UserId
            ).ToList();
            if(permissionEntities.Count == 0) {
              entry.Cancel = true;
            } else {
              db.RemoveRange(permissionEntities);
            }
        }
    }
}