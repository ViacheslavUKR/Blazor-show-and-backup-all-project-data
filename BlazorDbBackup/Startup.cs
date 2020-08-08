using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorDbBackup.Data;
using Microsoft.EntityFrameworkCore;
using BlazorDbBackup.Database;

namespace BlazorDbBackup
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public static IConfiguration Configuration { get; set; }
        public static IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddDbContext<ApplicationDbContext>(
                    (Microsoft.EntityFrameworkCore.DbContextOptionsBuilder options) =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    (Microsoft.EntityFrameworkCore.Infrastructure.MySqlDbContextOptionsBuilder mySqlOption) =>
                        {
                            mySqlOption.CommandTimeout(10);
                            mySqlOption.EnableRetryOnFailure(10);
                        }),
                    ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
