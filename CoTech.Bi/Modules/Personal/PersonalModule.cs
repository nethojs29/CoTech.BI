using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Personal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Personal{
    public class PersonalModule:IModule{
        public long Id{ get { return 20; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<PersonalRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<PersonalEntity>().ToTable("Personal");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){}

        public List<ISeed> ConfigureSeeds(BiContext context){
            return new List<ISeed>();
        }
    }
}