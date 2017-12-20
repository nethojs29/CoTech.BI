using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Repositories;
using System;
using CoTech.Bi.Core.Companies.Models;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Modules.Wer.Seeds;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using CoTech.Bi.Modules.Wer.EventProcessors;
using CoTech.Bi.Modules.Wer.Notifiers;

namespace CoTech.Bi.Modules.Wer
{
    public class WerModule: IModule
    {
        public long Id {
            get { return 6; }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RecurringJob.AddOrUpdate("crear semanas",(WeekRepository repository)=>repository.AddWeek(),Cron.Weekly(DayOfWeek.Saturday,20));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<WeekRepository>();
            services.AddScoped<ReportRepository>();
            services.AddScoped<FilesRepository>();
            services.AddScoped<ReplyRepository>();
            services.AddScoped<NotificationsIOSRepository>();
            services.AddSingleton(new ReportEventProcessor());
            services.AddSingleton(new ReportNotifier());
        }

        public void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeekEntity>().ToTable("Wer_Weeks");

            modelBuilder.Entity<ReportEntity>().ToTable("Wer_Reports");
            
            modelBuilder.Entity<FileEntity>().ToTable("Wer_File");
            
            modelBuilder.Entity<FileCompanyEntity>().ToTable("Wer_File_Company");
            
            modelBuilder.Entity<PartyEntity>().ToTable("Wer_Party");
            
            modelBuilder.Entity<GroupEntity>().ToTable("Wer_Groups");
            
            modelBuilder.Entity<SeenReportsEntity>().ToTable("Wer_Seen_Reports");
            
            modelBuilder.Entity<MessageEntity>().ToTable("Wer_Messages");
                        
            modelBuilder.Entity<FileEntity>().ToTable("Wer_File");
            
            modelBuilder.Entity<IOSTokenEntity>().ToTable("Wer_Token_User");
            
        }

        public void ConfigureInitializer(BiContext _context, UserManager<UserEntity> userManager)
        {
            // new InitializeModule(_context, userManager);
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed> { new WerSeed1(), new WerSeed1Weeks() };
        }
    }
}