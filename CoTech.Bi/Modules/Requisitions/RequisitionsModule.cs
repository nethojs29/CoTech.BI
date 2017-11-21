using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Requisitions.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Requisitions{
    public class RequisitionsModule : IModule{
        public long Id{ get { return 13; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<RequisitionRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<RequisitionEntity>().ToTable("Requisitions");
            modelBuilder.Entity<RequisitionFileEntity>().ToTable("Requisitions_Files");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed>();
        }
    }
}