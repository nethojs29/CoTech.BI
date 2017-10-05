using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.DinningRooms.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.DinningRooms{
    public class DinningRoomModule:IModule{
        public long Id{ get { return 10; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<DinningRoomRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<DinningRoomEntity>().ToTable("DinningRooms");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){
            
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed>();
        }
    }
}