using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Autofocus.Repository;
using Autofocus.Repository.IRepository;
using Autofocus.Mappers;
//using Autofocus.API.Mappers;

namespace Autofocus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //x => x.MigrationsAssembly("WebApplication")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
           .AddEntityFrameworkStores<ApplicationDbContext>();


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IEmailSender, EmailSender>();
            //webapi call
            services.AddScoped<IMainCategoryAPIRepository, MainCategoryAPIRepository>();
            services.AddScoped<IProductMasterAPIRepository, ProductMasterAPIRepository>();
            services.AddScoped<IUserRegistrationAPIRepository, UserRegistrationAPIRepository>();
          
            services.AddScoped<IUnitofWork, UnitofWork>();


           // services.AddScoped<ISP_CALL, SP_CALL>();
            services.AddAutoMapper(typeof(WinkoIndiaMappings));
            services.AddHttpClient();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
          //  services.AddApiVersioning();
            //  services.AddAutoMapper(typeof(WinkoIndiaMappings));
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            //app.UseEndpoints(routes =>
            //{
            //    routes.MapControllerRoute(
            //         name: "default",
            //         pattern: "{controller=Home1}/{action=Index}/{id?}");

            //    routes.MapAreaControllerRoute(
            //        name: "areas",
            //        areaName: "myarea",
            //            pattern: "{area:Admin}/{controller=Home}/{did?}/{action=Index}/{id?}");
            //    routes.MapRazorPages();
            //});
        }
    }
}
