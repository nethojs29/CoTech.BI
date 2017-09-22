using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Users.Controllers;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Clients.Models{
    public class ClientRepository{
        private readonly BiContext context;
        private DbSet<ClientEntity> db{
            get{return context.Set<ClientEntity>();}
        }

        public ClientRepository(BiContext context) {
            this.context = context;
        }
        
        public Task<List<ClientEntity>> getAll(){
            return db.Where( c => !c.DeletedAt.HasValue).ToListAsync();
        }
        public async Task Create(ClientEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }


    }
}