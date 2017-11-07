using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Budget.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Budget.Models{
    public class BudgetConceptRepository{
        private readonly BiContext context;

        private DbSet<BudgetConceptEntity> db{
            get { return context.Set<BudgetConceptEntity>(); }
        }

        public BudgetConceptRepository(BiContext context){
            this.context = context;
        }

        public Task<List<BudgetConceptEntity>> getAllByBudget(long budgetId){
            return db.Where(b => b.BudgetId == budgetId && !b.DeletedAt.HasValue).Include(b => b.ExpenseGroup).ToListAsync();
        }
        
        public Task<BudgetConceptEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(BudgetConceptEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateBudgetConceptReq entity){
            var concept = db.Find(id);
            context.Entry(concept).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(BudgetConceptEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}