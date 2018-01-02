using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Expenses.Controllers;
using CoTech.Bi.Modules.Requisitions.Models;
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

        public Task<List<ExpenseEntity>> getAllByRequisition(long requisitionId){
            return db.Where(p=> !p.DeletedAt.HasValue && p.RequisitionId == requisitionId).Include(e => e.Provider).Include(e => e.ExpenseGroup).ToListAsync();
        }

        public Task<List<ExpenseEntity>> getAllApprovedExpensesByGroupInYear(long group, int year){
            return db.Where(e => e.ExpenseGroupId == group && !e.DeletedAt.HasValue && e.Requisition.Status > 2 
                                 && e.Requisition.ApproveDate.Value.Year == year).ToListAsync();
        }

        public Task<List<ExpenseEntity>> getAllByGroupInMonth(long group, int year, int month){
            return db.Where(e => e.Requisition.Status >= 1 && e.ExpenseGroupId == group && !e.DeletedAt.HasValue && reqInMonth(e.Requisition, month, year)).ToListAsync();
        }

        public Task<List<ExpenseEntity>> getAllApprovedExpensesByTypeInYear(long type, int year){
            return db.Where(e => e.ExpenseGroup.TypeId == type && !e.DeletedAt.HasValue && e.Requisition.Status > 2 
                                 && e.Requisition.ApproveDate.Value.Year == year).ToListAsync();
        }
        
        public Task<List<ExpenseEntity>> getAllByTypeInMonth(long type, int year, int month){
            return db.Where(e => e.Requisition.Status >= 1 && e.ExpenseGroup.TypeId == type && !e.DeletedAt.HasValue && reqInMonth(e.Requisition, month, year)).ToListAsync();
        }
        
        public Task<List<ExpenseEntity>> getAllExpensesInMonth(int year, int month){
            return db.Where(e => e.Requisition.Status >= 1 && !e.DeletedAt.HasValue && reqInMonth(e.Requisition, month, year)).ToListAsync();
        }

        private bool reqInMonth(RequisitionEntity req, int month, int year){
            return req.ApplicationDate.Month == month && req.ApplicationDate.Year == year;
        }

        public Task<List<ExpenseEntity>> getAllExpensesByGroupInMonth(long groupId, int year, int month){
            return db.Where(e => e.Requisition.Status >= 1 && !e.DeletedAt.HasValue && reqInMonth(e.Requisition, month, year) && e.ExpenseGroupId == groupId).Include(e => e.Requisition).ToListAsync();
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