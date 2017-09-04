using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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