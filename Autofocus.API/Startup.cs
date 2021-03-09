using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Autofocus.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.DataAccess.Repository;
using Autofocus.API.Mappers;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Web.Services3.Referral;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Autofocus.API
{
    public class Startup
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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

            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddAutoMapper(typeof(WinkoIndiaMappings));
            //services.AddApiVersioning(options =>
            //{
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    options.ReportApiVersions = true;
            //});
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("WikoIndiaAPISpes",
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title= "MainCategory",
            //            Version="1"
            //        });
            //    //options.SwaggerDoc("WikoIndiaAPISpesSubCategory",
            //    //   new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //   {
            //    //       Title = "SubCategory",
            //    //       Version = "1"
            //    //   });
            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    options.IncludeXmlComments(cmlCommentsFullPath);
            //});
             
            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }





            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                    //options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json"),Desc
                    options.RoutePrefix = "";
                }


            });
            //foreach (var desc in provider.ApiVersionDescriptions)
            //{
            //    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());


            //    //options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json"),Desc

            //    options.RoutePrefix = "";
            //});
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/WikoIndiaAPISpes/swagger.json", "Wiko India");
            //    //options.SwaggerEndpoint("/swagger/WikoIndiaAPISpesSubCategory/swagger.json", "Sub Category");
            //    options.RoutePrefix = "";
            //});
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
