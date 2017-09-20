﻿using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
                return entity;
            }
            return null;
        }
        
    }
}