using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Providers.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Providers.Models{
    public class ProviderRepository{
        private readonly BiContext context;

        private DbSet<ProviderEntity> db{
            get { return context.Set<ProviderEntity>(); }
        }

        public ProviderRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ProviderEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<ProviderEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ProviderEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateProviderReq entity){
            var provider = db.Find(id);
            context.Entry(provider).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ProviderEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}