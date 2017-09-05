using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PermissionRepository(BiContext context)
        {
          this.context = context;
        }

        public Task<List<PermissionEntity>> GetUserPermissionsInCompany(long userId, long companyId){
          return db.Where(p => p.UserId == userId && p.CompanyId == companyId).ToListAsync();
        }

        public Task<bool> IsUserRoot(long userId) {
          return db.AnyAsync(p => p.UserId == userId && p.RoleId == (long)Role.Root);
        }

        public Task<bool> UserHasAtLeastOneRoleInCompany(long userId, long companyId, IEnumerable<long> roles) {
          return db.Where(p => p.UserId == userId && p.CompanyId == companyId && roles.Contains(p.RoleId))
            .AnyAsync();
        }

        public Task<bool> UserHasAtLeastOneRoleInCompanyOrIsRoot(long userId, long companyId, IEnumerable<long> roles){
          return db.Where(p => p.UserId == userId && 
              (p.RoleId == (long)Role.Root ||
                  ( p.CompanyId == companyId && roles.Contains(p.RoleId) )
              )
          ).AnyAsync();
        }
    }
}