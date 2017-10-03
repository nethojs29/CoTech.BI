using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Services{
    public class Service_Price_ClientRepository{
        private readonly BiContext context;

        private DbSet<Service_Price_ClientEntity> db{
            get { return context.Set<Service_Price_ClientEntity>(); }
        }

        public Service_Price_ClientRepository(BiContext context){
            this.context = context;
        }

        public Task<List<Service_Price_ClientEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<Service_Price_ClientEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(Service_Price_ClientEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateServicePriceClientReq entity){
            var service = db.Find(id);
            context.Entry(service).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(Service_Price_ClientEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}