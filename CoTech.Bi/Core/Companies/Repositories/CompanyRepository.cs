using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Core.Permissions.Models;
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
          return db.Where(c => !c.DeletedAt.HasValue)
            .Include(c => c.Modules).ToListAsync();
        }

        public async Task<List<CompanyEntity>> GetUserCompanies(long userId) {
          var permissions = await dbPermissions.Where(p => p.UserId == userId)
            .Include(p => p.Company)
              .ThenInclude(c => c.Modules)
            .Distinct()
            .ToListAsync();
          var companies = new List<CompanyEntity>();
          while(permissions.Count > 0) {
            var p = permissions.First();
            permissions.RemoveAt(0);
            companies.Add(p.Company);
            if (p.RoleId == 1) {
              var children = await db.Where(c => c.ParentId == p.CompanyId)
                .Include(c => c.Modules)
                .ToListAsync();
              children.ForEach(child => {
                permissions.Append(new PermissionEntity { 
                  CompanyId = child.Id,
                  RoleId = 1,
                  UserId = userId
                });
              });
              companies.AddRange(children);
            }
          }
          return companies.Distinct().ToList();
        }

        public Task<CompanyEntity> WithId(long id) {
          return db.Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<CompanyEntity> WithUrl(string url){
          return db.Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Url == url);
        }

        public Task<List<CompanyEntity>> ChildrenOf(long id) {
          return db.Where(c => c.ParentId == id)
            .Include(c => c.Modules)
            .ToListAsync();
        }

        public async Task<CompanyEntity> Create(CreateCompanyCmd cmd) {
          var evtEntity = CompanyCreatedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evtEntity);
          if(insertions == 0) return null;
          return await db.FirstAsync(c => c.CreatorEventId == evtEntity.Id);
        }

        public async Task<CompanyEntity> Update(UpdateCompanyCmd cmd){
          var evt = CompanyUpdatedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return await db.FirstAsync(c => c.Id == cmd.Id);
        }

        public async Task<bool> AddModule(AddModuleCmd cmd) {
          var evt = ModuleAddedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return insertions > 0;
        }

        public async Task<bool> RemoveModule(RemoveModuleCmd cmd) {
          var evt = ModuleRemovedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return insertions > 0;
        }

        public async Task<CompanyEntity> Delete(DeleteCompanyCmd cmd){
          var evt = CompanyDeletedEvt.MakeEventEntity(cmd);
          var insertions = await eventRepository.Create(evt);
          return await db.FirstAsync(c => c.Id == evt.Id);
        }
    }
}