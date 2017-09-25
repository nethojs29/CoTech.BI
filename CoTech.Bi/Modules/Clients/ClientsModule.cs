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
using System.Collections.Generic;

namespace CoTech.Bi.Modules.Clients {
    public class ClientModule : IModule
    {
        public long Id {
            get { return 7; }
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        public void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>().ToTable("Clients");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ClientRepository>();
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed>();
        }
    }
}