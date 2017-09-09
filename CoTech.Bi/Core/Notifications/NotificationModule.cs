using CoTech.Bi.Core.Notifications.Models;
using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Core.Notifications
{
  public class NotificationModule : IModule
  {
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }

    public void ConfigureEntities(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<NotificationEntity>().ToTable("Notifications")
        .Property("BodyJson").HasColumnName("Body");
      modelBuilder.Entity<ReceiverEntity>().ToTable("Notification_Receivers");
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<NotificationRepository>();
    }
  }
}