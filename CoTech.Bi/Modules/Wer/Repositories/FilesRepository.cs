using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class FilesRepository
    {
        private BiContext _context;

        private DbSet<FileEntity> _dbFiles
        {
            get { return this._context.Set<FileEntity>(); }
        }
        private DbSet<FileCompanyEntity> _dbFilesCompanies
        {
            get { return this._context.Set<FileCompanyEntity>(); }
        }
        private DbSet<ReportEntity> _dbReports
        {
            get { return this._context.Set<ReportEntity>(); }
        }
        private DbSet<WeekEntity> _dbWeeks
        {
            get { return this._context.Set<WeekEntity>(); }
        }

        public FilesRepository(BiContext biContext)
        {
            this._context = biContext;
        }

        public FileEntity CreateFile(FileEntity entity)
        {
            var file = entity;
            if (_dbReports.Count(r => r.Id == entity.ReportId) > 0)
            {
                _dbFiles.Add(file);
                _context.SaveChanges();
                return entity;
            }
            return null;
        }

        public Task<FileEntity> ById(long id)
        {
            return _dbFiles.FindAsync(id);
        }
        public bool? DeleteById(long id)
        {
            var file = _dbFiles.Find(id);
            if (file != null)
            {
                File.Delete(file.Uri);
                if (!File.Exists(file.Uri))
                {
                    _dbFiles.Remove(file);
                    return _context.SaveChanges() > 0;
                }
            }
            return null;
        }

        public FileCompanyEntity createFile(FileCompanyEntity file)
        {
            if (_dbWeeks.Any(w => w.Id == file.WeekId))
            {
                _dbFilesCompanies.Add(file);
                _context.SaveChanges();
                return file;
            }
            return null;
        }

        public List<LibraryResponse> GetLibrary(long idCompany,long idWeek)
        {
            var files = _dbFiles
                .Include(f => f.Report)
                .ThenInclude(f => f.User)
                .Where(
                f => f.Report.CompanyId == idCompany &&
                     f.Report.WeekId == idWeek
            ).OrderByDescending(f => f.Report.Week.EndTime).Select(
                data =>
                    new LibraryResponse()
                    {
                        Id = data.Id,
                        ReportId = data.ReportId,
                        WeekId = data.Report.WeekId,
                        UserId = data.Report.UserId,
                        Name = data.Name,
                        EndTime = data.Report.Week.EndTime,
                        StartTime = data.Report.Week.StartTime,
                        Type = data.Type,
                        Mime = data.Mime,
                        Username = data.Report.User.Name,
                        Userlastname = data.Report.User.Lastname
                    }
            ).ToList();
            var filesCompanies = _dbFilesCompanies.Where(f => f.CompanyId == idCompany && f.WeekId == idWeek)
                .OrderByDescending(f => f.Week.EndTime)
                .Select(data => new LibraryResponse()
                {
                    Id = data.Id,
                    ReportId = 0,
                    WeekId = data.WeekId,
                    UserId = data.UserId,
                    Name = data.Name,
                    EndTime = data.Week.EndTime,
                    StartTime = data.Week.StartTime,
                    Mime = data.Mime,
                    Username = data.User.Name,
                    Userlastname = data.User.Lastname,
                    Type = 2
                }).ToList();
            return files.Concat(filesCompanies).ToList();
        }
        public List<LibraryResponse> GetLibraryCompany(long idCompany)
        {
            var files =  _dbFiles
                .Include(f => f.Report)
                .ThenInclude(f => f.User)
                .Where(
                f => f.Report.CompanyId == idCompany)
                .OrderByDescending(f => f.Report.Week.EndTime).Select(
                data =>
                    new LibraryResponse()
                    {
                        Id = data.Id,
                        ReportId = data.ReportId,
                        WeekId = data.Report.WeekId,
                        UserId = data.Report.UserId,
                        Name = data.Name,
                        EndTime = data.Report.Week.EndTime,
                        StartTime = data.Report.Week.StartTime,
                        Type = data.Type,
                        Mime = data.Mime,
                        Username = data.Report.User.Name,
                        Userlastname = data.Report.User.Lastname
                    }
            ).ToList();
            var filesCompanies = _dbFilesCompanies.Where(f => f.CompanyId == idCompany)
                .OrderByDescending(f => f.Week.EndTime)
                .Select(data => new LibraryResponse()
                {
                    Id = data.Id,
                    ReportId = 0,
                    WeekId = data.WeekId,
                    UserId = data.UserId,
                    Name = data.Name,
                    EndTime = data.Week.EndTime,
                    StartTime = data.Week.StartTime,
                    Mime = data.Mime,
                    Username = data.User.Name,
                    Userlastname = data.User.Lastname,
                    Type = 2
                }).ToList();
            return files.Concat(filesCompanies).ToList();
        }
        
    }
}