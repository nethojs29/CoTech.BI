using CoTech.Bi.Loader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoTech.Bi.Modules.RES
{
    public class ResModule: IModule
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureEntities(ModelBuilder modelBuilder)
        {
            throw new System.NotImplementedException();
        }
    }
}