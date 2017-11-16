using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Requisitions.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Modules.Requisitions.Models{
    public class RequisitionRepository{
        private readonly BiContext context;

        private DbSet<RequisitionEntity> db{
            get { return context.Set<RequisitionEntity>(); }
        }

        public RequisitionRepository(BiContext context){
            this.context = context;
        }

        public Task<List<RequisitionEntity>> getAll(){
            return db.Where(p => !p.DeletedAt.HasValue).Include(r => r.Responsable).Include(r => r.DinningRoom).ToListAsync();
        }

        public Task<RequisitionEntity> WithId(long id){
            return db.Include(r => r.Responsable).Include(r => r.DinningRoom)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Create(RequisitionEntity entity){
            var count = db
                .Count(p => !p.DeletedAt.HasValue && p.CompanyId == entity.CompanyId && p.DinningRoomId == entity.DinningRoomId);
            entity.Keyword = String.Format("{0}{1}-{2}{3}", entity.DinningRoomId <= 9 ? "0" : "",entity.DinningRoomId, (count + 1) <= 9 ? "0" : "",count + 1);
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateRequisitionReq entity){
            var requisition = db.Find(id);
            Console.WriteLine(requisition);
            Console.WriteLine(entity);
            requisition.ApplicationDate = entity.getDate();
            requisition.ResponsableId = entity.ResponsableId;
            requisition.Total = entity.Total;
            requisition.DinningRoomId = entity.DinningRoomId;
            requisition.PaymentMethod = entity.PaymentMethod;
            db.Update(requisition);
            return context.SaveChangesAsync();
        }

        public async Task Approve(RequisitionEntity entity){
            db.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(RequisitionEntity entity){
            entity.DeletedAt = DateTime.Now;
            db.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}