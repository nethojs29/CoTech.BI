using System;
using CoTech.Bi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoTech.Bi
{
    public partial class bi_coreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User Id=bi;Password=bi-core;Database=bi-core");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
