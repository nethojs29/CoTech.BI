using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Controllers;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;
using CoTech.Bi.Modules.Clients.Controllers;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CoTech.Bi.Modules.Clients.Models{
    public class ClientRepository{
        private readonly BiContext context;
        private DbSet<ClientEntity> db{
            get{return context.Set<ClientEntity>();}
        }

        public ClientRepository(BiContext context) {
            this.context = context;
        }
        
        public Task<List<ClientEntity>> getAll(long idCompany){
            return db.Where( c => !c.DeletedAt.HasValue && c.CompanyId == idCompany).ToListAsync();
        }

        public Task<ClientEntity> withId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(ClientEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateClientReq entity){
            var client = db.Find(id);
            context.Entry(client).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(ClientEntity entity){
            entity.DeletedAt = DateTime.Today;
            db.Update(entity);
            await context.SaveChangesAsync();
        }


    }
}