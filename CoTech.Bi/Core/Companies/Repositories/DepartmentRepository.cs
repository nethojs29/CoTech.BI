using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Budget.Controllers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Companies.Repositories{
    public class DepartmentRepository{
        private readonly BiContext context;
        private readonly EventRepository eventRepo;

        private DbSet<DepartmentEntity> db{
            get { return context.Set<DepartmentEntity>(); }
        }

        public DepartmentRepository(BiContext context, EventRepository eventRepo){
            this.context = context;
            this.eventRepo = eventRepo;
        }

        public Task<List<DepartmentEntity>> getFromYear(long year){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<DepartmentEntity>> getAllByGroup(long group){
            return db.Where(p => !p.DeletedAt.HasValue).ToListAsync();
        }

        public Task<DepartmentEntity> WithId(long id){
            return db.FindAsync(id);
        }

        public async Task<DepartmentEntity> Create(CreateDepartmentCmd cmd){
            var evtEntity = DepartmentCreatedEvt.MakeEventEntity(cmd);
            var insertions = await eventRepo.Create(evtEntity);
            if (insertions == 0) return null;
            return await db.FirstAsync(d => d.CreatorEventId == evtEntity.Id);
        }

        public async Task<DepartmentEntity> Update(UpdateDepartmentCmd cmd){
            var evt = DepartmentUpdatedEvt.MakeEventEntity(cmd);
            var insertions = await eventRepo.Create(evt);
            return await db.FirstAsync(d => d.Id == evt.Id);
        }

        public async Task<bool> Delete(DeleteDepartmentCmd cmd){
            var evt = DepartmentDeletedEvt.MakeEventEntity(cmd);
            var insertions = await eventRepo.Create(evt);
            return insertions > 0;
        }
    }
}