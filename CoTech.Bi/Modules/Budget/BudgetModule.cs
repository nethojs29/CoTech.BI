using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Budget.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Budget{
    public class BudgetModule:IModule{
        public long Id{
            get { return 9; }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<BudgetRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<BudgetEntity>().ToTable("Budgets");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed>();
        }
    }
}