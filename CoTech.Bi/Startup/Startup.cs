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
using CoTech.Bi.Rx;
using CoTech.Bi.Loader;
using CoTech.Bi.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

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
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                // options.Audience = "http://localhost:5001/"; // web host
                // options.Authority = "http://localhost:5000/"; // api host
                // options.RequireHttpsMetadata = false;
                // options.TokenValidationParameters = new TokenValidationParameters()
				// {
				// 	ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
				// 	ValidAudience = Configuration["JwtSecurityToken:Audience"],
				// 	ValidateIssuerSigningKey = true,
				// 	IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityToken:Key"])),
				// 	ValidateLifetime = true
				// };
            });
            // requires using Microsoft.AspNetCore.Mvc;
            services.AddCors();
            services.AddSingleton<EventEmitter>();
            services.AddBiModules();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseBiModules(env);
            app.UseMvc();
        }
    }
}
