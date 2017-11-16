using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Sales.Models;
using CoTech.Bi.Modules.Sales.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Sales{
    public class SalesModule:IModule{
        public long Id{ get { return 19; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<ServiceSaleRepository>();
            services.AddScoped<SSaleRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<ServiceSaleEntity>().ToTable("Daily_Service_Sale");
            modelBuilder.Entity<SSaleEntity>().ToTable("Service_Sales");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){}

        public List<ISeed> ConfigureSeeds(BiContext context){
            return new List<ISeed>();
        }
    }
}