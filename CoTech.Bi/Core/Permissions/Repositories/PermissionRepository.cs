using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
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

        public PermissionRepository(BiContext context)
        {
          this.context = context;
        }

        public Task<List<PermissionEntity>> GetUserPermissionsInCompany(long userId, long companyId){
          return db.Where(p => p.UserId == userId && p.CompanyId == companyId).ToListAsync();
        }

        public async Task Create(PermissionEntity entity) {
          db.Add(entity);
          await context.SaveChangesAsync();
        }

        public Task<PermissionEntity> FindOne(long companyId, long userId, long roleId) {
          return db.FirstAsync(p => p.CompanyId == companyId && p.UserId == userId && p.RoleId == roleId);
        }

        public async Task Delete(PermissionEntity entity){
          db.Remove(entity);
          await context.SaveChangesAsync();
        }

        public async Task Revoke(List<PermissionEntity> entities) {
          db.RemoveRange(entities);
          await context.SaveChangesAsync();
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