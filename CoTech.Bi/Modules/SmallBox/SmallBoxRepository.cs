﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.SmallBox.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.SmallBox{
    public class SmallBoxRepository{
        private readonly BiContext context;

        private DbSet<SmallBoxEntity> db{
            get { return context.Set<SmallBoxEntity>(); }
        }

        public SmallBoxRepository(BiContext context){
            this.context = context;
        }

        public Task<List<SmallBoxEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<SmallBoxEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(SmallBoxEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateSmallBoxEntryReq entity){
            var entry = db.Find(id);
            context.Entry(entry).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(SmallBoxEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}