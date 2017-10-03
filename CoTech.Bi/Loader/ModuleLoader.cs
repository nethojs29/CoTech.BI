using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoTech.Bi.Loader
{
    public static class ModuleLoader {

        private static List<IModule> modules = new List<IModule>();

        public static void LoadModules() {
            Assembly consoleAssembly = Assembly.GetExecutingAssembly();
            List<Type> moduleTypes = GetTypesByInterface<IModule>(consoleAssembly);
            foreach(Type moduleType in moduleTypes)
            {
                IModule module = Activator.CreateInstance(moduleType) as IModule;
                var moduleWithSameId = modules.Find(m => m.Id == module.Id);
                if(moduleWithSameId != null) {
                    throw new Exception($"Id de módulo repetido. {moduleWithSameId.GetType().Name}, {moduleType.Name}({module.Id})");
                }
                modules.Add(module);
            }
        }
        public static void UseBiModules(this IApplicationBuilder app, IHostingEnvironment env){
            modules.ForEach(m => m.Configure(app, env));
        }
        public static void AddBiModules(this IServiceCollection services){
            modules.ForEach(m => m.ConfigureServices(services));
        }
        public static void BiEntities(this ModelBuilder modelBuilder){
            modules.ForEach(m => m.ConfigureEntities(modelBuilder));
        }
        public static void BiInitialize(this BiContext biContext, UserManager<UserEntity> _manager)
        {
            modules.ForEach(item => item.ConfigureInitializer(biContext,_manager));
        }
        public static void BiSeedUp(this MigrationBuilder migrationBuilder, int version) {
            var context = new BiContext();
            modules.ForEach(mod => mod.ConfigureSeeds(context)
                .Where(s => s.Version == version)
                .ToList()
                .ForEach(s => s.Up(context)));
        }
        public static void BiSeedDown(this MigrationBuilder migrationBuilder, int version) {
            var context = new BiContext();
            modules.ForEach(mod => mod.ConfigureSeeds(context)
                .Where(s => s.Version == version)
                .ToList()
                .ForEach(s => s.Down(context)));
        }
        private static List<Type> GetTypesByInterface<T>(Assembly assembly)
        {
            if (!typeof(T).IsInterface)
                throw new ArgumentException("T must be an interface");
        
            return assembly.GetTypes()
                .Where(x => x.GetInterface(typeof(T).Name) != null)
                .ToList<Type>();
        }

    }
}