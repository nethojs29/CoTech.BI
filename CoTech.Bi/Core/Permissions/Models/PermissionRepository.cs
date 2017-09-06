using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Identity.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Permissions.Model
{
    public class PermissionRepository
    {
        private readonly BiContext context;
        private DbSet<PermissionEntity> db {
          get { return context.Set<PermissionEntity>(); }
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

        public Task<bool> UserIsRoot(long userId) {
          return db.AnyAsync(p => p.UserId == userId && p.RoleId == (long)Role.Root);
        }

        public Task<bool> UserHasAtLeastOneRoleInCompany(long userId, long companyId, IEnumerable<long> roles, bool orRoot){
          if(!orRoot) {
            return db.Where(p => p.UserId == userId && p.CompanyId == companyId && roles.Contains(p.RoleId))
            .AnyAsync();
          }
          return db.Where(p => p.UserId == userId && 
              (p.RoleId == (long)Role.Root ||
                  ( p.CompanyId == companyId && roles.Contains(p.RoleId) )
              )
          ).AnyAsync();
        }


        public async Task<bool> UserHasAtLeastOneRoleInCompany(long userId, long companyId, IEnumerable<long> roles, bool orRoot, bool orSuperInAncestor){
          var hasRole = await UserHasAtLeastOneRoleInCompany(userId, companyId, roles, orRoot);
          if(!orSuperInAncestor) return hasRole;
          if(hasRole) return true;
          var company = await dbCompany.FindAsync(companyId);
          if(!company.Parent.HasValue) return false;
          return await UserHasAtLeastOneRoleInCompany(userId, company.Parent.Value, new long[]{Role.Super}, false, true); // ya sabemos que no es root
        }

        public Task<bool> UserHasAnyRoleInCompany(long userId, long companyId, bool orRoot){
          if(orRoot) {
            return db.Where(p => p.UserId == userId && (p.CompanyId == companyId || p.RoleId == Role.Root))
              .AnyAsync();
          }
          return db.Where(p => p.UserId == userId && p.CompanyId == companyId).AnyAsync();
        }

        public async Task<bool> UserHasAnyRoleInCompany(long userId, long companyId, bool orRoot, bool orSuperInAncestor){
          var hasAnyRole = await UserHasAnyRoleInCompany(userId, companyId, orRoot);
          if(!orSuperInAncestor) return hasAnyRole;
          if(hasAnyRole) return true;
          var company = await dbCompany.FindAsync(companyId);
          if(!company.Parent.HasValue) return false;
          return await UserHasAtLeastOneRoleInCompany(userId, company.Parent.Value, new long[]{Role.Super}, false, true);
        }
    }
}