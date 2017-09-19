using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Requests;
using CoTech.Bi.Modules.Wer.Models.Responses;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Remotion.Linq.Clauses;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class ReportRepository
    {
        
        private BiContext context;
        
        private DbSet<ReportEntity> db{
            get
            {
                return this.context.Set<ReportEntity>();
            }
        }
        
        private DbSet<WeekEntity> dbWeek{
            get
            {
                return this.context.Set<WeekEntity>();
            }
        }

        private DbSet<PermissionEntity> dbPermissionEntities
        {
            get { return this.context.Set<PermissionEntity>(); }
        }

        private DbSet<UserEntity> dbUsersEntities
        {
            get { return this.context.Set<UserEntity>(); }
        }

        private DbSet<CompanyEntity> dbCompaniesEntities
        {
            get { return this.context.Set<CompanyEntity>(); }
        }

        private WeekRepository _weekRepository;

        private CompanyRepository _companyRepository;
        
        public ReportRepository(BiContext context, WeekRepository repository,CompanyRepository companyRepository){
            this.context = context;
            this._weekRepository = repository;
            this._companyRepository = companyRepository;
        }

        public Task<List<ReportEntity>> getAll()
        {
            return db.ToListAsync();
        }
        public async Task<List<ReportEntity>> byWeek(long? week)
        {
            if (_weekRepository.Exist(week))
            {
                return await db.Where(r => r.WeekId == week).ToListAsync();
            }
            return await db.Where(r => r.WeekId == ( _weekRepository.Current()).Id).ToListAsync();
        }
        public Task<List<IGrouping<long, ReportEntity>>> byUser(int user)
        {
            return db.Where(r => r.UserId == user).OrderByDescending(r => r.WeekId)
                .GroupBy(r => r.CompanyId)
                .ToListAsync();
        }

        public async Task<ReportEntity> Create(ReportRequest request)
        {
            var report = new ReportEntity()
            {
                UserId = request.UserId,
                Financial = request.Financial,
                Observation = request.Observation,
                Operative = request.Operative,
                CompanyId = request.CompanyId,
                WeekId = request.WeekId
            };
            db.Add(report);
            await context.SaveChangesAsync();
            return report;
        }

        public bool Delete(long IdReport)
        {
            var report = db.Find(IdReport);
            if (report != null)
            {
                db.Remove(report);
                if (context.SaveChanges()>0)
                {
                    return true; 
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Task<ReportEntity> byIdReport(long IdReport)
        {
            return db.FindAsync(IdReport);
        }

        public List<ReportEntity> FilterBetweenWeeks(long start, long end)
        {
            var startWeek = dbWeek.Find(start);
            var endWeek = dbWeek.Find(end);
            if (startWeek != null && endWeek != null)
            {
                return db.Where(r => r.WeekId >= startWeek.Id).Where(rep => rep.WeekId <= endWeek.Id).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ReportEntity>> byWeekRecursive(long idCompany,long? idWeek)
        {
            if (!_weekRepository.Exist(idWeek))
            {
                idWeek = (await _weekRepository.Current()).Id;
            }
            var example = await db.Where(r => r.Company.ParentId == idCompany && r.WeekId == idWeek).OrderBy(r => r.CompanyId).ToListAsync();
            var childern = await _companyRepository.ChildrenOf(idCompany);

            foreach (CompanyEntity companyEntity in childern)
            {
                var returned = await this.byWeekRecursive(companyEntity.Id, idWeek);
                example = example.Concat(returned).ToList();
            }
            return example;
        }

        public Task<List<PermissionEntity>> getPermissions(long User)
        {
            return dbPermissionEntities.Where(r => r.UserId == User && (r.RoleId > 600 && r.RoleId < 605)).ToListAsync();
        }
        
        public Task<List<ReportEntity>> getByUserCompany(long company, long user)
        {
            return db.Where(r => r.CompanyId == company && r.UserId == user)
                .OrderByDescending(r => r.WeekId)
                .ToListAsync();
        }

        public List<CompanyResponse> GetCompaniesRecursive(long idCompany)
        {
            List<CompanyResponse> list = new List<CompanyResponse>();
            var company = dbCompaniesEntities.Include(c => c.Children).FirstOrDefault(c => c.Id == idCompany);
            if (company != null)
            {
                var users = dbUsersEntities
                    .Where(u => u.Permissions.Any(p => p.CompanyId == idCompany && (p.RoleId > 600 && p.RoleId < 700)))
                    .Include(u => u.Permissions.Where(p => p.RoleId > 600 && p.RoleId < 700)).ToList();
                var userAndPermission = users.Select(u => new WerUserAndPermissions(u, u.Permissions)).ToList();
                list.Add(new CompanyResponse(){company = company, Users = userAndPermission});
                foreach (CompanyEntity child in company.Children)
                {
                    var returned = this.GetCompaniesRecursive(child.Id);
                    list = list.Concat(returned).ToList();
                }
            }
            return list;
        }
    }
}