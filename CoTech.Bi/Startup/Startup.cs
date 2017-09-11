using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoTech.Bi.Loader;
using CoTech.Bi.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using CoTech.Bi.Core.Users.Models;
using CoTech.Bi.Core.Permissions.Model;
using Microsoft.AspNetCore.Identity;
using CoTech.Bi.Identity.DataAccess;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using CoTech.Bi.Util;

namespace CoTech.Bi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ModuleLoader.LoadModules();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddEntityFrameworkMySql();
            services.AddDbContext<BiContext>();
            services.AddIdentity<UserEntity, Role>()
                .AddDefaultTokenProviders();
            services.AddTransient<IUserStore<UserEntity>, UserStorage>();
            services.AddTransient<IRoleStore<Role>, RoleStorage>();
            services.AddSingleton<JwtTokenGenerator>();
            services.Configure<IdentityOptions>(options => {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                // options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            
            
            // requires using Microsoft.AspNetCore.Mvc;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Bi API", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "BiApi.xml"); 
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors();
            services.AddBiModules();
            services.AddMvc();
            
            services.AddScoped<IDbInitializer, DbInitializer>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IDbInitializer dbInitializer)
        {
            app.UseWebSockets();
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bi API V1");
            });

            
            app.UseBiModules(env);
            app.UseMvc();
            
            dbInitializer.Initialize().Wait();

        }
    }
}
