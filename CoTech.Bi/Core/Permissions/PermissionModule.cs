using CoTech.Bi.Core.Permissions.Model;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Core.Permissions
{
  public class PermissionModule : IModule
  {
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {}

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PermissionEntity>().ToTable("Permissions");
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<PermissionRepository>();
    }
  }
}