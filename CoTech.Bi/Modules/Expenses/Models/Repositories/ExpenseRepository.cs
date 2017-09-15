using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Expenses.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseRepository{
        private readonly BiContext context;

        private DbSet<ExpenseEntity> db{
            get { return context.Set<ExpenseEntity>(); }
        }

        public ExpenseRepository(BiContext context){
            this.context = context;
        }
        
        public Task<List<ExpenseEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<ExpenseEntity> WithId(long id){
            return db.Include(r => r.ExpenseGroup).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Create(ExpenseEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateExpenseReq entity){
            var expense = db.Find(id);
            context.Entry(expense).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ExpenseEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}