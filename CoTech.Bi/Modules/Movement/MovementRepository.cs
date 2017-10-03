﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Movement.Controllers;
using CoTech.Bi.Modules.Movement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Movement{
    public class MovementRepository{
        private readonly BiContext context;

        private DbSet<MovementEntity> db{
            get { return context.Set<MovementEntity>(); }
        }

        public MovementRepository(BiContext context){
            this.context = context;
        }

        public Task<List<MovementEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<MovementEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(MovementEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateMovementReq entity){
            var provider = db.Find(id);
            context.Entry(provider).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(MovementEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}