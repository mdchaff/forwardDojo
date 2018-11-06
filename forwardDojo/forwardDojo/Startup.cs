using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forwardDojo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace forwardDojo
{
    public class Startup
    {
        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }
        public IConfiguration Configuration{
            get;
        }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options => options.UseMySQL(Configuration["DbInfo:ConnectionString"]));
            System.Console.WriteLine("\n\n\t==== NEW BUILD ====\n\n");
            services.AddMvc ();
            services.AddSession();
            // services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles ();
            app.UseSession();
            app.UseMvc ();
        }
    }
}





//WeddingPlanner