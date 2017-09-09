using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Users.Models
{
    public class UserRepository
    {
        private readonly BiContext context;
        private DbSet<UserEntity> db {
          get { return context.Set<UserEntity>(); }
        }

        public UserRepository(BiContext context)
        {
          this.context = context;
        }

        public Task<List<UserEntity>> GetAll() {
          return db.Where(u => !u.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<UserEntity>> InCompany(long companyId){
          return db.Where(u => u.Permissions.Any(p => p.CompanyId == companyId))
            .ToListAsync();
        }

        public Task<List<UserEntity>> GetRootUsers() {
          return db.Where(u => u.Root != null).ToListAsync();
        }
    }
}