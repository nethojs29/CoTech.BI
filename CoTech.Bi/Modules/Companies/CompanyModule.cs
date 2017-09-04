using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Companies.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Companies
{
  public class CompanyModule : IModule
  {
    void IModule.Configure(IApplicationBuilder app, IHostingEnvironment env)
    {}

    void IModule.ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CompanyEntity>().ToTable("Companies");
    }

    void IModule.ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<CompanyRepository>();
    }
  }
}