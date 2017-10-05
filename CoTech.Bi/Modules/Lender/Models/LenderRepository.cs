using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Lender.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Lender.Models{
    public class LenderRepository{
        private readonly BiContext context;

        private DbSet<LenderEntity> db{
            get { return context.Set<LenderEntity>(); }
        }

        public LenderRepository(BiContext context){
            this.context = context;
        }

        public Task<List<LenderEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<LenderEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(LenderEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateLenderReq entity){
            var lender = db.Find(id);
            context.Entry(lender).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(LenderEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}