using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Clients.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Banks.Models{
    public class BankRepository{
        private readonly BiContext context;

        private DbSet<BankEntity> db{
            get { return context.Set<BankEntity>(); }
        }

        public BankRepository(BiContext context){
            this.context = context;
        }

        public Task<List<BankEntity>> getAll(){
            return db.Where(b => !b.DeletedAt.HasValue).ToListAsync();
        }

        public Task<BankEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(BankEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateBankReq req){
            var bank = db.Find(id);
            context.Entry(bank).CurrentValues.SetValues(req);
            return context.SaveChangesAsync();
        }

        public async Task Delete(BankEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}