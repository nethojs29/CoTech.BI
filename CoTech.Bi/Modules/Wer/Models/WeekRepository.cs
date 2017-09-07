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
        
    }
}