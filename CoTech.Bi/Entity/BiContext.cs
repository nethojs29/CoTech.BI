using System;
using CoTech.Bi.Authorization;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoTech.Bi.Entity
{
    public partial class BiContext : IdentityDbContext<UserEntity, RoleEntity, long>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=192.168.1.190;User Id=bi;Password=bi-core;Database=bi-core");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BiEntities();
        }
    }
}
