using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Core.Notifications.Repositories;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Core.Notifications
{
  public class NotificationModule : IModule
  {
    public long Id {
      get { return -5; }
    }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<NotificationEntity>().ToTable("Notifications")
        .Property("BodyJson").HasColumnName("Body");
      modelBuilder.Entity<ReceiverEntity>().ToTable("Notification_Receivers");
    }

    public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
    {
      
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<NotificationRepository>();
    }
  }
}