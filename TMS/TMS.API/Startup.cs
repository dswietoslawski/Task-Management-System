using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .AddEntityFrameworkStores<TmsContext>();

            svcs.AddMvc()
                .AddJsonOptions(opts => {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            svcs.AddScoped<TmsInitializer>();
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

            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            app.UseIdentity();
            app.UseMvc();

            initializer.SeedAsync().Wait();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
