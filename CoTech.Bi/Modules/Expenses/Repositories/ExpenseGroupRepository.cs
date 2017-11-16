using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Expenses.Controllers;
using CoTech.Bi.Modules.Providers.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseGroupRepository{
        private readonly BiContext context;

        private DbSet<ExpenseGroupEntity> db{
            get { return context.Set<ExpenseGroupEntity>(); }
        }

        public ExpenseGroupRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ExpenseGroupEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<ExpenseGroupEntity>> getAllByType(long typeId){
            return db.Where(e => !e.DeletedAt.HasValue && e.TypeId == typeId).ToListAsync();
        }

        public Task<ExpenseGroupEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ExpenseGroupEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateExpenseGroupReq entity){
            var group = db.Find(id);
            context.Entry(group).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ExpenseGroupEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}