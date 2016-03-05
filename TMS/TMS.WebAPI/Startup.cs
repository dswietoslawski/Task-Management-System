using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using TMS.WebAPI.Infrastructure;

namespace TMS.WebAPI {
    public class Startup {

        public void Configuration(IAppBuilder app) {
            HttpConfiguration httpConfig = new HttpConfiguration();
            ConfigureOAuthTokenConfiguration(app);
            ConfigureWebApi(httpConfig);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        private void ConfigureWebApi(HttpConfiguration httpConfig) {
            httpConfig.MapHttpAttributeRoutes();

            var jsonFormatter = httpConfig.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        private void ConfigureOAuthTokenConfiguration(IAppBuilder app) {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
        }
    }
}