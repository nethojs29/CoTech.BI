using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Expenses.Controllers;
using CoTech.Bi.Modules.Providers.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Expenses.Models{
    public class ExpenseTypeRepository{
        private readonly BiContext context;

        private DbSet<ExpenseTypeEntity> db{
            get { return context.Set<ExpenseTypeEntity>(); }
        }

        public ExpenseTypeRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ExpenseTypeEntity>> getAll(long companyId){
            return db.Where(p => !p.DeletedAt.HasValue && p.CompanyId == companyId).ToListAsync();
        }

        public Task<ExpenseTypeEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ExpenseTypeEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateExpenseTypeReq entity){
            var type = db.Find(id);
            context.Entry(type).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ExpenseTypeEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}