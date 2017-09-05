using System.Threading.Tasks;
using CoTech.Bi.Entity;
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

        public Task<PermissionEntity> GetUserPermissionInCompany(long userId, long companyId){
          return db.FirstAsync(p => p.CompanyId == companyId && p.UserId == userId);
        }
    }
}