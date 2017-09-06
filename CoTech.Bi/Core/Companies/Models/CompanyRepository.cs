using System.Collections.Generic;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Companies.Models
{
    public class CompanyRepository
    {
        private readonly BiContext context;
        private DbSet<CompanyEntity> db {
          get { return context.Set<CompanyEntity>(); }
        }

        public CompanyRepository(BiContext context)
        {
          this.context = context;
        }

        public Task<List<CompanyEntity>> GetAll() {
          return db.ToListAsync();
        }

        public Task<CompanyEntity> WithUrl(string url){
          return db.FirstOrDefaultAsync(c => c.Url == url);
        }

        public async Task Create(CompanyEntity entity) {
          var entry = db.Add(entity);
          await context.SaveChangesAsync();
        }
    }
}