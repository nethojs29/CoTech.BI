using CoTech.Bi.Loader;
using CoTech.Bi.Modules.DinningRooms.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.DinningRooms{
    public class DinningRoomModule:IModule{
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<DinningRoomRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<DinningRoomEntity>().ToTable("DinningRooms");
        }
    }
}