using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Sales.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Sales.Repositories{
    public class ServiceSaleRepository{
        private readonly BiContext context;

        private DbSet<ServiceSaleEntity> db{
            get { return context.Set<ServiceSaleEntity>(); }
        }

        public ServiceSaleRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ServiceSaleEntity>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && idCompany == p.CompanyId).ToListAsync();
        }
        
        public Task<List<ServiceSaleEntity>> getAllByClient(long idClient){
            return db.Where(p => !p.DeletedAt.HasValue && idClient == p.Sale.ClientId).Include(s => s.Sale).ToListAsync();
        }

        public Task<ServiceSaleEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ServiceSaleEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateServiceSaleReq entity){
            var sale = db.Find(id);
            context.Entry(sale).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ServiceSaleEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}