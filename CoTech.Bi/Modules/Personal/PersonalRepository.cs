using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Personal.Controllers;
using CoTech.Bi.Modules.Personal.Models;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Personal{
    public class PersonalRepository{
        private readonly BiContext context;

        private DbSet<PersonalEntity> db{
            get { return context.Set<PersonalEntity>(); }
        }

        public PersonalRepository(BiContext context){
            this.context = context;
        }

        public Task<List<PersonalEntity>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && p.CompanyId == idCompany).ToListAsync();
        }

        public Task<PersonalEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(PersonalEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdatePersonalReq entity){
            var provider = db.Find(id);
            context.Entry(provider).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(PersonalEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}