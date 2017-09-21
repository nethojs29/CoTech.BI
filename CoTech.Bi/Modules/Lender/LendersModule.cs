using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Lender.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Lender{
    public class LendersModule:IModule{
        public long Id{ get { return 14; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
        }

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<LenderRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<LenderEntity>().ToTable("Lenders");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){
        }
    }
}