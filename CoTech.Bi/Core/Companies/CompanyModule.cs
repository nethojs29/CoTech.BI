using CoTech.Bi.Loader;
using CoTech.Bi.Core.Companies.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoTech.Bi.Core.Companies.Repositories;
using CoTech.Bi.Core.Companies.Notifiers;
using CoTech.Bi.Core.Companies.EventProcessors;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Identity;

namespace CoTech.Bi.Core.Companies
{
  public class CompanyModule : IModule
  {
    public long Id { 
      get { return 2; } 
    }
    void IModule.Configure(IApplicationBuilder app, IHostingEnvironment env)
    {}

    void IModule.ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CompanyEntity>().ToTable("Companies")
          .HasIndex(c => c.Url)
          .IsUnique();
      modelBuilder.Entity<CompanyToModule>().ToTable("Company_Has_Modules");
    }

    public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
    {
      
    }

    void IModule.ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<CompanyRepository>();
      services.AddScoped<CompanyNotifier>();
      services.AddSingleton(new CompanyEventProcessor());
    }
  }
}