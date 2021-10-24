using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace poc_asp_net_core_redis.Extensions
{
    public static class ApplicationBuildExtensions
    {
        public static IApplicationBuilder UseApiInfra(this IApplicationBuilder builder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
            }

            builder.UseHttpsRedirection();

            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            builder.UseForwardedHeaders(forwardOptions);

            builder.UseRouting();

            builder.UseAuthentication();
            builder.UseAuthorization();

            builder.UseSwagger();
            builder.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Poc Asp.net core redis API");
                c.RoutePrefix = string.Empty;

            });

            builder.UseStaticFiles();

            builder.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            return builder;
        }
    }
}
