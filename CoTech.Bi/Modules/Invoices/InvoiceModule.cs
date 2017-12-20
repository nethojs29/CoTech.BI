using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Invoices.Models;
using CoTech.Bi.Modules.Invoices.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Invoices{
    public class InvoiceModule : IModule{
        public long Id => 22;
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<InvoiceRepository>();
        }

        public void ConfigureEntities(ModelBuilder mBuilder){
            mBuilder.Entity<InvoiceEntity>().ToTable("Invoices");
        }
        
        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){}

        public List<ISeed> ConfigureSeeds(BiContext context){
            return new List<ISeed>();
        }
    }
}