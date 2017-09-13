using System;
using CoTech.Bi.Authorization;
using CoTech.Bi.Loader;
using CoTech.Bi.Core.Users.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CoTech.Bi.Entity;
using CoTech.Bi.Identity.DataAccess;
using CoTech.Bi.Modules.Clients.Models;
using CoTech.Bi.Modules.Providers.Models;

namespace CoTech.Bi.Modules.Providers{
    public class ProviderModule : IModule{
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
            
        }

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<ProviderRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<ProviderEntity>().ToTable("Providers");
        }
    }
}