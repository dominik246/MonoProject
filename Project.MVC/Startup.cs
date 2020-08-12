using Autofac;
using Autofac.Features.ResolveAnything;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Project.MVC.Controllers;
using Project.Service.DataAccess;

using System;
using System.Linq;
using System.Reflection;

namespace Project.MVC
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public ILifetimeScope AutofacContainer { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc().AddControllersAsServices();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder().UseSqlServer(Configuration.GetConnectionString("default"));

            builder
                .RegisterGeneric(typeof(VehicleService<>))
                .As(typeof(IVehicleService<>))
                .InstancePerLifetimeScope();
            builder
                .RegisterType<ServiceDBContext>()
                .As<IServiceDBContext>()
                .WithParameter("options", dbContextOptionsBuilder.Options)
                .InstancePerLifetimeScope();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
