﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Sales.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Sales.Repositories{
    public class SSaleRepository{
        private readonly BiContext context;

        private DbSet<SSaleEntity> db{
            get { return context.Set<SSaleEntity>(); }
        }

        public SSaleRepository(BiContext context){
            this.context = context;
        }

        public Task<List<SSaleEntity>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && idCompany == p.CompanyId).Include(p => p.Client).ToListAsync();
        }

        public Task<List<SSaleEntity>> getAllInMonth(long idCompany, int month, int year){
            return db.Where(p => !p.DeletedAt.HasValue && idCompany == p.CompanyId && p.Date.Month == month && p.Date.Year == year).ToListAsync();
        }

        public Task<List<SSaleEntity>> getAllInYear(long idCompany, int year){
            return db.Where(p => !p.DeletedAt.HasValue && idCompany == p.CompanyId && p.Date.Year == year).ToListAsync();
        }
        
        public Task<List<SSaleEntity>> getAllByClient(long idClient){
            return db.Where(s => !s.DeletedAt.HasValue && s.ClientId == idClient).Include(s => s.Client).ToListAsync();
        }

        public Task<SSaleEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(SSaleEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateSSaleRequest entity){
            var sale = db.Find(id);
            context.Entry(sale).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(SSaleEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}