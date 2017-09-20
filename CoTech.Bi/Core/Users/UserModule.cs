using System;
using CoTech.Bi.Authorization;
using CoTech.Bi.Loader;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Identity.DataAccess;
using CoTech.Bi.Core.Users.Repositories;
using CoTech.Bi.Core.Users.EventProcessors;

namespace CoTech.Bi.Core.Users
{
  public class UserModule : IModule
  {
    public long Id {
      get { return 1; }
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserEntity>().ToTable("Users")
        .HasIndex(u => u.Email)
        .IsUnique();
    }

    public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
    {
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<UserRepository>();
      services.AddSingleton(new UserEventProcessor());
    }
  }
}