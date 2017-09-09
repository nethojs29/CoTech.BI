using System.Collections.Generic;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Wer.Models
{
    public class ReportRepository
    {
        
        private BiContext context;
        
        private DbSet<ReportEntity> db{
            get { return this.context.Set<ReportEntity>(); }
        }
        
        public ReportRepository(BiContext context){
            this.context = context;
        }

        public Task<List<ReportEntity>> getAll()
        {
            return db.ToListAsync();
        }
    }
}