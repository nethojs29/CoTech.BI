using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class ReportRepository
    {
        
        private BiContext context;
        
        private DbSet<ReportEntity> db{
            get { return this.context.Set<ReportEntity>(); }
        }
        
        private DbSet<WeekEntity> dbWeek{
            get { return this.context.Set<WeekEntity>(); }
        }
        
        public ReportRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ReportEntity>> getAll()
        {
            return db.ToListAsync();
        }
        public Task<List<ReportEntity>> byWeek(int? week)
        {
            if (week == null)
            {
                week = int.Parse(dbWeek.OrderByDescending(w => w.EndTime).First().Id.ToString());
            }
            return db.Where(r => r.WeekId == week).ToListAsync();
        }
        public Task<List<ReportEntity>> byUser(int user)
        {
            return db.Where(r => r.User == user).ToListAsync();
        }
    }
}