using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Services{
    public class ServiceRepository{
        private readonly BiContext context;

        private DbSet<ServiceEntity> db{
            get { return context.Set<ServiceEntity>(); }
        }

        public ServiceRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ServiceEntity>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && idCompany == p.CompanyId).ToListAsync();
        }

        public Task<ServiceEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ServiceEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateServiceReq entity){
            var service = db.Find(id);
            context.Entry(service).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ServiceEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}