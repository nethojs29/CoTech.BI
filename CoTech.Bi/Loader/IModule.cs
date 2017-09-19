using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Loader
{
    public interface IModule
    {
        void Configure(IApplicationBuilder app, IHostingEnvironment env);
        void ConfigureServices(IServiceCollection services);
        void ConfigureEntities(ModelBuilder modelBuilder);
        void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager);
    }
}