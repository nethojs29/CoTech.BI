using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.EntityFrameworkCore;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Events;
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

        private DbSet<SeenReportsEntity> dbSeenReportsEntities
        {
            get { return this.context.Set<SeenReportsEntity>(); }
        }

        private WeekRepository _weekRepository;

        private CompanyRepository _companyRepository;
        private EventRepository _eventRepository;
        
        public ReportRepository(BiContext context, WeekRepository repository,CompanyRepository companyRepository, EventRepository eventRepository){
            this.context = context;
            this._weekRepository = repository;
            this._companyRepository = companyRepository;
            this._eventRepository = eventRepository;
        }

        public Task<List<ReportPdfResponse>> getPdfData(long idCompany, long idWeek)
        {
            return db.Where(r => r.CompanyId == idCompany && r.WeekId == idWeek).Select(
                r =>
                    new ReportPdfResponse()
                    {
                        
                    }
            ).ToListAsync();
        }

        public async Task<ReportEntity> SearchOrCreate(long idCompany, long idUser, long idWeek,long idCreator)
        {
            var report = await db.Include(r => r.Seen).Include(r => r.User).Include(r => r.Files).Where(r => r.WeekId == idWeek && r.UserId == idUser && r.CompanyId == idCompany)
                .FirstOrDefaultAsync();
            if (report == null)
            {
                report = new ReportEntity()
                {
                    CompanyId = idCompany,
                    UserId = idUser,
                    WeekId = idWeek
                };
                db.Add(report);
                context.SaveChanges();
                report.Seen = new List<SeenReportsEntity>();
                report.Files = new List<FileEntity>();
            }
            if(report != null)
                if (report.Id > 0 && !report.Seen.Exists(s => s.UserId == idCreator))
                {
                    dbSeenReportsEntities.Add(new SeenReportsEntity()
                    {
                        Report = report,
                        UserId = idCreator
                    });
                    context.SaveChanges();
                    var aux = db.Include(r => r.Seen).Include(r => r.User).Include(r => r.Files).FirstOrDefault(r=> r.Id == report.Id);
                    report = aux;
                    return report;
                }
            return report;
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
            var Seen = new List<SeenReportsEntity>();
            Seen.Add(new SeenReportsEntity()
            {
                UserId = request.UserId,
                Report = report
            });
            report.Seen = Seen;
            db.Add(report);
            await context.SaveChangesAsync();
            return report;
        }
        public async Task<ReportEntity> Update(ReportRequest request,long idreport,long idUser)
        {
            var evt = new EventEntity()
            {
                UserId = idUser,
                Body = new ReportUpdatedEvt()
                {
                    Id = idreport,
                    Observation = request.Observation,
                    Operative = request.Operative,
                    Financial = request.Financial
                }
            };
            var updating = await _eventRepository.Create(evt);
            if (updating == 0)
            {
                return null;
            }
            else
            {
                return db.Include(r => r.User).Include(r => r.Files).Include(r => r.Seen).First(r => r.Id == idreport);
            }

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
        
        public List<ReportPdfResponse> GetReportRecursive(long idCompany, long idWeek)
        {
            List<ReportPdfResponse> list = new List<ReportPdfResponse>();
            var company = dbCompaniesEntities.Include(c => c.Children).FirstOrDefault(c => c.Id == idCompany);
            if (company != null)
            {
                var users = dbUsersEntities
                    .Where(u => u.Permissions.Where(p => p.RoleId > 600 && p.RoleId < 700).Any(p => p.CompanyId == idCompany))
                    .ToList();
                foreach (var user in users)
                {
                    var report = db.Include(r => r.Company).Include(r => r.User).Include(r => r.Week).FirstOrDefault(r =>
                        r.UserId == user.Id && r.CompanyId == idCompany && r.WeekId == idWeek);
                    if (report != null)
                    {
                        string color = "";
                        if (!string.IsNullOrEmpty(report.Company.Color))
                        {
                            color = report.Company.Color;
                        }
                        list.Add(new ReportPdfResponse()
                        {
                            EndTime = report.Week.EndTime,
                            StarTime = report.Week.StartTime,
                            Finantial = report.Financial,
                            Operative = report.Operative,
                            Observations = report.Observation,
                            User = report.User.Name + " " + report.User.Lastname,
                            Company = report.Company.Name,
                            Color = color
                        });
                    }
                    else
                    {
                        var week = dbWeek.First(w => w.Id == idWeek);
                        list.Add(new ReportPdfResponse()
                        {
                            EndTime = week.EndTime,
                            StarTime = week.StartTime,
                            Finantial = "",
                            Operative = "",
                            Observations = "",
                            User = user.Name + " " + user.Lastname,
                            Company = company.Name,
                            Color = company.Color
                        });
                    }
                }
                
                foreach (CompanyEntity child in company.Children)
                {
                    var returned = this.GetReportRecursive(child.Id,idWeek);
                    list = list.Concat(returned).ToList();
                }
            }
            return list;
        }
        public List<ReportPendingsResponse> GetReportSeensRecursive(long idCompany, long idUser)
        {
            var week = dbWeek.OrderByDescending(r => r.EndTime).Take(10).ToList();
            List<ReportPendingsResponse> list = new List<ReportPendingsResponse>();
            var company = dbCompaniesEntities.Include(c => c.Children).FirstOrDefault(c => c.Id == idCompany);
            if (company != null)
            {
                var users = dbUsersEntities
                    .Where(u => u.Permissions.Where(p => p.RoleId > 600 && p.RoleId < 700).Any(p => p.CompanyId == idCompany))
                    .ToList();
                foreach (var user in users)
                {
                    var report = db
                        .Where(r => r.Seen.Count(rp => rp.UserId == idUser) == 0)
                        .Include(r => r.Company)
                        .Include(r => r.User)
                        .Include(r => r.Week)
                        .Include(r => r.Seen)
                        .FirstOrDefault();
                    if (report != null)
                    {
                        if (string.IsNullOrEmpty(report.Financial) || string.IsNullOrEmpty(report.Operative))
                        {
                            list.Add(new ReportPendingsResponse()
                            {
                                idWeek = report.WeekId,
                                idCompany = report.CompanyId,
                                idUser = report.UserId,
                                User = report.User.Name + " " + report.User.Lastname,
                                create = false
                            });
                        }
                        else
                        {
                            list.Add(new ReportPendingsResponse()
                            {
                                idWeek = report.WeekId,
                                idCompany = report.CompanyId,
                                idUser = report.UserId,
                                User = report.User.Name + " " + report.User.Lastname,
                                create = true
                            });
                        }
                    }
                    else if(company.ParentId != null)
                    {
                        foreach (var weekEntity in week)
                        {
                            var reportData = db.FirstOrDefault(r =>
                                r.WeekId == weekEntity.Id &&
                                r.UserId == user.Id &&
                                r.CompanyId == company.Id
                            );
                            if (reportData != null)
                            {
                                list.Add(new ReportPendingsResponse()
                                {
                                    idWeek = weekEntity.Id,
                                    idCompany = company.Id,
                                    idUser = user.Id,
                                    User = user.Name + " " + user.Lastname,
                                    create = false
                                });
                            }
                            else
                            {
                                list.Add(new ReportPendingsResponse()
                                {
                                    idWeek = weekEntity.Id,
                                    idCompany = company.Id,
                                    idUser = user.Id,
                                    User = user.Name + " " + user.Lastname,
                                    create = false
                                });
                            }
                        }
                    }
                }
                
                foreach (CompanyEntity child in company.Children)
                {
                    var returned = GetReportSeensRecursive(child.Id,idUser);
                    list = list.Concat(returned).ToList();
                }
            }
            return list;
        }

        public List<CompanyResponse> GetCompaniesRecursive(long idCompany)
        {
            List<CompanyResponse> list = new List<CompanyResponse>();
            var company = dbCompaniesEntities.Include(c => c.Children).FirstOrDefault(c => c.Id == idCompany);
            if (company != null)
            {
                var users = dbUsersEntities
                    .Where(u => u.Permissions.Where(p => p.RoleId > 600 && p.RoleId < 700).Any(p => p.CompanyId == idCompany))
                    .Include(u => u.Permissions).ToList();
                List<WerUserAndPermissions> userAndPermission = users.Select(u => new WerUserAndPermissions(u)).ToList();
                list.Add(new CompanyResponse()
                {
                    company = new CompanyEntity()
                    {
                        Id = company.Id,
                        Name = company.Name,
                        Color = company.Color,
                        Activity = company.Activity,
                        Url = company.Url,
                        PhotoUrl = company.PhotoUrl,
                        ParentId = company.ParentId
                    },
                    Users = userAndPermission
                });
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