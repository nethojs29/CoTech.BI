using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.SmallBox.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.SmallBox{
    public class SmallBoxModule:IModule{
        public long Id{ get { return 18; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<SmallBoxRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<SmallBoxEntity>().ToTable("SmallBox");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){}

        public List<ISeed> ConfigureSeeds(BiContext context){
            return new List<ISeed>();
        }
    }
}