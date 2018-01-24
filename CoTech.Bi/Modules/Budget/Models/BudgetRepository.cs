﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Budget.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Budget.Models{
    public class BudgetRepository{
        private readonly BiContext context;

        private DbSet<BudgetEntity> db{
            get { return context.Set<BudgetEntity>(); }
        }

        public BudgetRepository(BiContext context){
            this.context = context;
        }

        public Task<List<BudgetEntity>> getFromYear(long year, long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && p.Year == year && p.CompanyId == idCompany).ToListAsync();
        }

        public Task<List<BudgetEntity>> monthly(long year, long month, long dinningRoom,  long idCompany){
            return db.Where(b => !b.DeletedAt.HasValue && b.Year == year && b.Month == month && b.CompanyId == idCompany && b.DinningRoomId == dinningRoom).OrderBy(b => b.ExpenseTypeId).ToListAsync();
        }

        public Task<List<BudgetEntity>> monthlyBudget(long year, long month, long idCompany){
            return db.Where(b => !b.DeletedAt.HasValue && b.Year == year && b.Month == month && b.CompanyId == idCompany && b.ExpenseTypeId != 1).OrderBy(b => b.ExpenseTypeId).ToListAsync();
        }

        public Task<List<BudgetEntity>> getAllByGroup(long type, int year){
            return db.Where(p => !p.DeletedAt.HasValue && p.ExpenseTypeId == type && p.Year == year).ToListAsync();
        }

        public Task<BudgetEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(BudgetEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateBudgetReq entity){
            var budget = db.Find(id);
            context.Entry(budget).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(BudgetEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}