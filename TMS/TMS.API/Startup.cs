using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

using Newtonsoft.Json.Serialization;
using TMS.API.Infrastructure;
using TMS.API.Models;
using TMS.API.Repositories;
using TMS.API.ViewModels;
using TMS.API.Utilities.Extensions;

namespace TMS.API {
    public class Startup {
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv) {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                    .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection svcs) {
            svcs.AddInstance(_config);

            svcs.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<TmsContext>();

            svcs.AddIdentity<TmsUser, IdentityRole>()
                .AddEntityFrameworkStores<TmsContext>()
                .AddDefaultTokenProviders();

            svcs.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()));


            svcs.AddMvc()
                .AddJsonOptions(opts => {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            svcs.AddScoped<TmsInitializer>();
            svcs.AddScoped<ITaskRepository, TaskRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, TmsInitializer initializer) {
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            if (_env.IsDevelopment()) {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(options => options.ShowExceptionDetails = true);
            } else {
                loggerFactory.AddDebug(LogLevel.Error);
                app.UseExceptionHandler("/Error");
            }

            app.UseJwtAuthentication();
            app.UseOpenIdConnectServer(options =>
            {
                options.AllowInsecureHttp = true;
                options.AuthorizationEndpointPath = PathString.Empty;
                options.TokenEndpointPath = "/connect/token";

                options.Provider = new AuthorizationProvider();
            });

            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            Mapper.Initialize(config => {
                config.CreateMap<UserTask, UserTaskViewModel>().ReverseMap();
            });

            app.UseIdentity();
            app.UseCors("AllowAll");
            app.UseMvc();

            initializer.SeedAsync().Wait();
        }


        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
