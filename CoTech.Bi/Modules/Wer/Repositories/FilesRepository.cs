using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace CoTech.Bi.Modules.Wer.Repositories
{
    public class FilesRepository
    {
        private BiContext _context;

        private DbSet<FileEntity> _dbFiles
        {
            get { return this._context.Set<FileEntity>(); }
        }
        private DbSet<ReportEntity> _dbReports
        {
            get { return this._context.Set<ReportEntity>(); }
        }

        public FilesRepository(BiContext biContext)
        {
            this._context = biContext;
        }

        public async Task<FileEntity> CreateFile(FileEntity entity)
        {
            var file = entity;
            if (_dbReports.Count(r => r.Id == entity.ReportId) > 0)
            {
                _dbFiles.Add(file);
                await _context.SaveChangesAsync();
                return new FileEntity(){Mime = file.Mime, Id = file.Id, ReportId = file.ReportId, Name = file.Name};
            }
            return null;
        }

        public Task<FileEntity> ById(long id)
        {
            return _dbFiles.FindAsync(id);
        }
        
    }
}