using System;
using CoTech.Bi.Authorization;
using CoTech.Bi.Loader;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Entity;
using CoTech.Bi.Identity.DataAccess;

namespace CoTech.Bi.Core.Users
{
  public class UserModule : IModule
  {
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserEntity>().ToTable("Users");
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentity<UserEntity, Role>()
        .AddDefaultTokenProviders();
      services.AddTransient<IUserStore<UserEntity>, UserStorage>();
      services.AddTransient<IRoleStore<Role>, RoleStorage>();
      services.AddScoped<UserRepository>();
      services.Configure<IdentityOptions>(options => {
          // Password settings
          options.Password.RequireDigit = false;
          options.Password.RequiredLength = 4;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
          options.Password.RequireLowercase = false;

          // Lockout settings
          // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
          // options.Lockout.MaxFailedAccessAttempts = 10;

          // User settings
          options.User.RequireUniqueEmail = true;
      });
    }
  }
}