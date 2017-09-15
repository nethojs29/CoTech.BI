using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Expenses.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.Expenses{
    public class ExpenseModule : IModule{
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
    }
}