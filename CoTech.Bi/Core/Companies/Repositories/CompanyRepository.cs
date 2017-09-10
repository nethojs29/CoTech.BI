using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Companies.Repositories
{
    public class CompanyRepository
    {
        private readonly BiContext context;
        private DbSet<CompanyEntity> db {
          get { return context.Set<CompanyEntity>(); }
        }
        private DbSet<PermissionEntity> dbPermissions {
          get { return context.Set<PermissionEntity>(); }
        }

        public CompanyRepository(BiContext context)
        {
          this.context = context;
        }

        public Task<List<CompanyEntity>> GetAll() {
          return db.Where(c => !c.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<CompanyEntity>> GetUserCompanies(long userId) {
          return dbPermissions.Where(p => p.UserId == userId)
            .Select(p => p.Company)
            .Distinct()
            .ToListAsync();
        }

        public Task<CompanyEntity> WithId(long id) {
          return db.FindAsync(id);
        }

        public Task<CompanyEntity> WithUrl(string url){
          return db.FirstOrDefaultAsync(c => c.Url == url);
        }

        public Task<List<CompanyEntity>> ChildrenOf(long id) {
          return db.Where(c => c.ParentId == id).ToListAsync();
        }

        public async Task Create(CompanyEntity entity) {
          var entry = db.Add(entity);
          await context.SaveChangesAsync();
        }

        public async Task Update(CompanyEntity entity){
          var entry = db.Update(entity);
          entry.Property(e => e.DeletedAt).IsModified = false;
          await context.SaveChangesAsync();
        }

        public async Task Delete(CompanyEntity entity){
          entity.DeletedAt = new DateTime();
          db.Update(entity);
          await context.SaveChangesAsync();
        }
    }
}