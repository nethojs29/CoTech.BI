using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Wer
{
    public class WerModule: IModule
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //throw new System.NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<WeekRepository>();
            services.AddScoped<ReportRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeekEntity>().ToTable("Wer_Weeks");

            modelBuilder.Entity<ReportEntity>().ToTable("Wer_Reports");
        }
    }
}