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
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<RequisitionEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task Create(RequisitionEntity entity){
            var entry = db.Add(entity);
            await context.SaveChangesAsync();
        }

        public Task<int> Update(long id, UpdateRequisitionReq entity){
            var requisition = db.Find(id);
            context.Entry(requisition).CurrentValues.SetValues(entity);
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