using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Entity;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace CoTech.Bi.Core.Companies.Repositories
{
    public class CompanyRepository
    {
        private readonly BiContext context;
        private readonly EventRepository eventRepository;

        private DbSet<CompanyEntity> db {
          get { return context.Set<CompanyEntity>(); }
        }
        private DbSet<PermissionEntity> dbPermissions {
          get { return context.Set<PermissionEntity>(); }
        }

        public CompanyRepository(BiContext context, EventRepository eventRepository)
        {
          this.context = context;
          this.eventRepository = eventRepository;
        }

        public Task<List<CompanyEntity>> GetAll() {
          return db.Where(c => !c.DeletedAt.HasValue).ToListAsync();
        }

        public Task<List<CompanyEntity>> GetUserCompanies(Guid userId) {
          return dbPermissions.Where(p => p.UserId == userId)
            .Select(p => p.Company)
            .Distinct()
            .ToListAsync();
        }

        public Task<CompanyEntity> WithId(Guid id) {
          return db.FindAsync(id);
        }

        public Task<CompanyEntity> WithUrl(string url){
          return db.FirstOrDefaultAsync(c => c.Url == url);
        }

        public Task<List<CompanyEntity>> ChildrenOf(Guid id) {
          return db.Where(c => c.ParentId == id).ToListAsync();
        }

        public async Task<CompanyEntity> Create(CompanyCreatedEvt evt, Guid userId) {
          var evtEntity = new EventEntity {
            Id = Guid.NewGuid(),
            UserId = userId,
            Body = evt
          };
          await eventRepository.Create(evtEntity);
          return await db.FirstAsync(c => c.Id == evt.Id);
        }

        public async Task<CompanyEntity> Update(CompanyUpdatedEvt evt, Guid userId){
          var evtEntity = new EventEntity {
            Id = Guid.NewGuid(),
            UserId = userId,
            Body = evt
          };
          await eventRepository.Create(evtEntity);
          return await db.FirstAsync(c => c.Id == evt.Id);
        }

        public async Task<CompanyEntity> Delete(CompanyDeletedEvt evt, Guid userId){
          var evtEntity = new EventEntity {
            Id = Guid.NewGuid(),
            UserId = userId,
            Body = evt
          };
          await eventRepository.Create(evtEntity);
          return await db.FirstAsync(c => c.Id == evt.Id);
        }
    }
}