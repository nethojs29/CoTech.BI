using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Identity.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Permissions.Repositories
{
    public class PermissionRepository
    {
        private readonly BiContext context;
        private DbSet<PermissionEntity> db {
          get { return context.Set<PermissionEntity>(); }
        }
        private DbSet<RootEntity> dbRoot {
          get { return context.Set<RootEntity>(); }
        }
        private DbSet<CompanyEntity> dbCompany {
          get { return context.Set<CompanyEntity>(); }
        }
        private EventRepository eventRepository;

        public PermissionRepository(BiContext context, EventRepository eventRepo)
        {
          this.context = context;
          this.eventRepository = eventRepo;
        }

        public async Task<PermissionEntity> Create(GivePermissionCmd cmd) {
          var evtEntity = PermissionGivenEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evtEntity);
          if(insertions == 0) return null;
          return await db.FirstAsync(p => p.CreatorEventId == evtEntity.Id);
        }
        public async Task<bool> Delete(RemoveRoleCmd cmd){
          var evt = RoleRemovedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return insertions > 0;
        }

        public async Task<bool> Revoke(RevokePermissionsCmd cmd) {
          var evt = PermissionsRevokedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return insertions > 0;
        }
        public Task<List<PermissionEntity>> GetUserPermissionsInCompany(long userId, long companyId){
          return db.Where(p => p.UserId == userId && p.CompanyId == companyId).ToListAsync();
        }

        public Task<PermissionEntity> FindOne(long companyId, long userId, long roleId) {
          return db.FirstAsync(p => p.CompanyId == companyId && p.UserId == userId && p.RoleId == roleId);
        }


        #region Funciones booleanas para autorizacion

        public Task<bool> UserIsRoot(long userId) {
          return dbRoot.AnyAsync(p => p.UserId == userId);
        }

        public async Task<bool> UserHasAtLeastOneRoleAnywhere(long userId, IEnumerable<long> roles, bool orRoot) {
          var permissionQuery = db.Where(p => p.UserId == userId && roles.Contains(p.RoleId));
          if (orRoot) {
            if(await UserIsRoot(userId)) return true;
          }
          return await permissionQuery.AnyAsync();
        }

        public async Task<bool> UserHasAtLeastOneRoleInCompany(long userId, long companyId, IEnumerable<long> roles, bool orIsRoot){
          var permissionQuery = db.Where(p => p.UserId == userId && p.CompanyId == companyId && roles.Contains(p.RoleId));
          if (orIsRoot) {
            if(await UserIsRoot(userId)) return true;
          }
          return await permissionQuery.AnyAsync();
        }

        public async Task<bool> UserHasAtLeastOneRoleInCompany(long userId, long companyId, IEnumerable<long> roles, bool orIsRoot, bool orIsSuperInAncestor){
          var hasRole = await UserHasAtLeastOneRoleInCompany(userId, companyId, roles, orIsRoot);
          if(!orIsSuperInAncestor) return hasRole;
          if(hasRole) return true;
          var company = await dbCompany.FindAsync(companyId);
          if(!company.ParentId.HasValue) return false;
          return await UserHasAtLeastOneRoleInCompany(userId, company.ParentId.Value, new long[]{Role.Super}, false, true); // ya sabemos que no es root
        }

        public async Task<bool> UserHasAnyRoleInCompany(long userId, long companyId, bool orIsRoot){
          var permissionQuery = db.Where(p => p.UserId == userId && p.CompanyId == companyId);
          if (orIsRoot) {
            if(await UserIsRoot(userId)) return true;
          }
          return await permissionQuery.AnyAsync();
        }

        public async Task<bool> UserHasAnyRoleInCompany(long userId, long companyId, bool orIsRoot, bool orIsSuperInAncestor){
          var hasAnyRole = await UserHasAnyRoleInCompany(userId, companyId, orIsRoot);
          if(!orIsSuperInAncestor) return hasAnyRole;
          if(hasAnyRole) return true;
          var company = await dbCompany.FindAsync(companyId);
          if(!company.ParentId.HasValue) return false;
          return await UserHasAtLeastOneRoleInCompany(userId, company.ParentId.Value, new long[]{Role.Super}, false, true);
        }

        #endregion
    }
}