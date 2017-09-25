using System.Collections.Generic;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Expenses.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Expenses{
    public class ExpenseModule : IModule{
        public long Id{ get { return 11; } }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){}

        public void ConfigureServices(IServiceCollection services){
            services.AddScoped<ExpenseRepository>();
            services.AddScoped<ExpenseTypeRepository>();
            services.AddScoped<ExpenseGroupRepository>();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder){
            modelBuilder.Entity<ExpenseEntity>().ToTable("Expenses");
            modelBuilder.Entity<ExpenseTypeEntity>().ToTable("ExpenseTypes");
            modelBuilder.Entity<ExpenseGroupEntity>().ToTable("ExpensesGroups");
        }

        public void ConfigureInitializer(BiContext context, UserManager<UserEntity> userManager){
        }

        public List<ISeed> ConfigureSeeds(BiContext context)
        {
            return new List<ISeed>();
        }
    }
}