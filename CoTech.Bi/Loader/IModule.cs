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
        /// <summary>
        /// Número que identifica al módulo, ademas de el rango de roles
        /// para el módulo. Ej: Id = 3, Roles del 300 al 399
        /// </summary>
        /// <returns>Identificador del módulo</returns>
        long Id { get; }
        void Configure(IApplicationBuilder app, IHostingEnvironment env);
        void ConfigureServices(IServiceCollection services);
        void ConfigureEntities(ModelBuilder modelBuilder);
        void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager);
    }
}