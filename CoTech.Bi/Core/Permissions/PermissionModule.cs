using CoTech.Bi.Core.Permissions.EventProcessors;
using CoTech.Bi.Core.Permissions.Models;
using CoTech.Bi.Core.Permissions.Repositories;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Core.Permissions
{
  public class PermissionModule : IModule
  {
    public long Id {
      get { return 3; }
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {}

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PermissionEntity>().ToTable("Permissions");
      modelBuilder.Entity<RootEntity>().ToTable("RootUsers");
    }

    public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
    {
      
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<PermissionRepository>();
      services.AddSingleton(new PermissionEventProcessor());
    }
  }
}