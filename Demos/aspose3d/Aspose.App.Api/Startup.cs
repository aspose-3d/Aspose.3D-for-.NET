using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.App.Api.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Aspose.App.Api.Services;
using Aspose.ThreeD;

namespace Aspose.App.Api
{
    public class Startup
    {
        private ILogger _logger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            }
           );
            //services.AddMvc();
            services.AddControllers();
           
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    ;

                });
            });
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AsposeApp Interfaces",
                    Description = "RESTful API for AsposeApp.",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Aspose", Email = "asposeapp@163.com", Url = new Uri("https://3d.aspose.app") }
                });

                option.OperationFilter<SwaggerFileUploadFilter>();
                option.OperationFilter<SwaggerFileUploadListFilter>();
                
                //Set the comments path for the swagger json and ui.
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "AsposeApp.xml");
                //option.IncludeXmlComments(xmlPath);
            });
            
            services.AddSingleton(typeof(StorageService));
            services.AddSingleton(typeof(MeasurementService));
            services.AddSingleton(typeof(StatsService));
            services.AddSingleton(typeof(ConholdateService));
            services.AddHttpClient("forum", c =>
            {
                c.DefaultRequestHeaders.Add("User-Agent", "Aspose.3D App");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.Timeout = TimeSpan.FromSeconds(10);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<Startup>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _logger.LogInformation("Starting in development mode");
            }
            else
                _logger.LogInformation("Starting in production mode");

            var aspose3DVersion = typeof(Aspose.ThreeD.Scene).Assembly.GetName().Version;
            _logger.LogInformation($"Aspose.3D Version : {aspose3DVersion}");

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseRouting();
            app.UseCors("any");  
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.ShowExtensions();
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "AsposeApp V1");
            });
            var workingDirectory = Configuration["SystemConfig:WorkingDirectory"];
            Console.WriteLine("Working directory : {0}", workingDirectory);
            Console.WriteLine("Forum URL : {0}", Configuration["SystemConfig:ForumUrl"]);
            if (File.Exists("Aspose.Total.lic"))
            {
                _logger.LogInformation("Applying license");
                var lic = new Aspose.ThreeD.License();
                lic.SetLicense("Aspose.Total.lic");
            }
            else
            {
                _logger.LogInformation("License file is missing, enter evaluation mode.");
            }
        }
    }
}
