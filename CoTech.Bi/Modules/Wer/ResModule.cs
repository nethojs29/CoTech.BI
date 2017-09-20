using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Repositories;
using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Initialize;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Wer
{
    public class WerModule: IModule
    {
        public long Id {
            get { return 6; }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RecurringJob.AddOrUpdate("crear semanas",(WeekRepository repository)=>repository.AddWeek(),Cron.Weekly(DayOfWeek.Saturday));
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

        public void ConfigureInitializer(BiContext _context, UserManager<UserEntity> userManager)
        {
            new InitializeModule(_context, userManager);
        }
    }
}