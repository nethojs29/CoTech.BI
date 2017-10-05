using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.DinningRooms.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.DinningRooms.Models{
    public class DinningRoomRepository{
        private readonly BiContext context;

        private DbSet<DinningRoomEntity> db{
            get { return context.Set<DinningRoomEntity>(); }
        }

        public DinningRoomRepository(BiContext context){
            this.context = context;
        }

        public Task<List<DinningRoomEntity>> getAll(long idCompany){
            return db.Where(p => !p.DeletedAt.HasValue && p.CompanyId == idCompany).ToListAsync();
        }

        public Task<DinningRoomEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(DinningRoomEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateDinningRoomReq entity){
            var provider = db.Find(id);
            context.Entry(provider).CurrentValues.SetValues(entity);
            return context.SaveChangesAsync();
        }

        public async Task Delete(DinningRoomEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}