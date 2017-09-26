using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Services.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Services{
    public class ServicesModule:IModule{
        public long Id{ get { return 16; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<ServiceRepository>();
            services.AddScoped<Service_Price_ClientRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<ServiceEntity>().ToTable("Services");
            modelBuilder.Entity<Service_Price_ClientEntity>().ToTable("Services_Price_Clients");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){}
    }
}