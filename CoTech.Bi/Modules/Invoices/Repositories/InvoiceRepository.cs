using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Invoices.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Invoices.Repositories{
    public class InvoiceRepository{
        private readonly BiContext context;

        private DbSet<InvoiceEntity> db => context.Set<InvoiceEntity>();

        public InvoiceRepository(BiContext context){
            this.context = context;
        }

        public Task<List<InvoiceEntity>> getAll(long idCompany){
            return db.Where(i => !i.DeletedAt.HasValue && i.CompanyId == idCompany).ToListAsync();
        }
        
        public Task<InvoiceEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(InvoiceEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }
        
        public async Task Delete(InvoiceEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}