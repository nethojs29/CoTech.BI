using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Modules.Wer.Models;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Models
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

        public void AddWeek()
        {
            var monday = DateTime.Now.AddDays(2);
            var friday = DateTime.Now.AddDays(6);
            db.Add(new WeekEntity(){StartTime = monday,EndTime = friday});
            context.SaveChanges();
        }

    }
}