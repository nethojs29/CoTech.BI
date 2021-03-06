﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Entity;
using CoTech.Bi.Util;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class WeekRepository{
        private BiContext context;

        private DbSet<WeekEntity> db{
            get { return this.context.Set<WeekEntity>(); }
        }
        
        public WeekRepository(BiContext context){
            this.context = context;
        }

        public Task<List<WeekEntity>> getAll()
        {
            return db.ToListAsync();
        }

        public Task<WeekEntity> withId(long id) {
            return db.FindAsync(id);
        }

        public void AddWeek()
        {
            var monday = DateTime.Now.AddDays(2);
            var friday = DateTime.Now.AddDays(6);
            db.Add(new WeekEntity(){StartTime = monday,EndTime = friday});
            context.SaveChanges();
        }

        public Task<PaginateList<WeekEntity>> paginateWeeks(int? page)
        {
            var weeks = db.OrderByDescending(w => w.EndTime).AsQueryable();
            return PaginateList<WeekEntity>.CreateAsync(weeks, page ?? 1, 54);
        }

        public Task<WeekEntity> Current()
        {
            return db.OrderByDescending(w => w.EndTime).FirstAsync();
        }

        public bool Exist(long? id)
        {
            if (id != null)
            {
                 return db.Where(w => w.Id == id).Count() > 0 ? true: false;
            }
            return false;
        }

    }
}