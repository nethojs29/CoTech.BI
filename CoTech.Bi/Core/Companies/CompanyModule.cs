using CoTech.Bi.Loader;
using CoTech.Bi.Core.Companies.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Companies.Notifiers;

namespace CoTech.Bi.Core.Companies
{
  public class CompanyModule : IModule
  {
    void IModule.Configure(IApplicationBuilder app, IHostingEnvironment env)
    {}

    void IModule.ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CompanyEntity>().ToTable("Companies")
          .HasIndex(c => c.Url)
          .IsUnique();
    }

    void IModule.ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<CompanyRepository>();
      services.AddScoped<CompanyNotifier>();
    }
  }
}