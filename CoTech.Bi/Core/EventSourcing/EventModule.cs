using CoTech.Bi.Core.EventSourcing.Models;
using CoTech.Bi.Core.EventSourcing.Repositories;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Core.EventSourcing
{
  public class EventModule : IModule
  {
    public long Id {
      get { return 4; }
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>().ToTable("Events")
            .Property("BodyJson").HasColumnName("Body");
    }

    public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
    {
      
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<EventRepository>();
    }
  }
}