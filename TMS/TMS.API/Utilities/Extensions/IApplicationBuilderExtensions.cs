using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.API.Utilities.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseJwtAuthentication(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
                {
                    if (context.Request.QueryString.HasValue)
                    {
                        var token = context.Request.QueryString.Value
                            .Split('&')
                            .SingleOrDefault(x => x.Contains("authorization"))?.Split('=')[1];

                        if (!string.IsNullOrWhiteSpace(token))
                        {
                            context.Request.Headers.Add("Authorization", new[] { $"Bearer {token}" });
                        }
                    }
                }
                await next.Invoke();
            });
            app.UseJwtBearerAuthentication(options =>
            {
                options.Audience = "localhost:49687";
                options.Authority = "localhost:49687";
                options.Configuration.TokenEndpoint = "localhost:49687/api/token";
                options.AutomaticAuthenticate = true;
            });
        }
    }
}
