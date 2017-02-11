using AutoMapper;
using DotNetLive.Framework.WebFramework.Filters;
using DotNetLive.Framework.DependencyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetLive.Framework.DependencyRegister
{
    public class MvcDependencyRegister : IDependencyRegister
    {
        public void Register(IServiceCollection services, IConfigurationRoot configuration, IServiceProvider serviceProvider)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(new GlobalDbTransactionAttribute() { Order = 0 });
                setupAction.Filters.Add(new GlobalExceptionAttribute() { Order = 99 });
            });

            services.AddAutoMapper();

            // Added - uses IOptions<T> for your settings.
            services.AddOptions();

            services.AddDataProtection();

            // Add memory cache services.
            services.AddMemoryCache(setup =>
            {
                setup.ExpirationScanFrequency = TimeSpan.FromMinutes(1);
            });

            services.AddDistributedMemoryCache();

            services.AddMvc(setup => { });

            services.AddRouting(routeOptions =>
            {
                routeOptions.AppendTrailingSlash = true;
                routeOptions.LowercaseUrls = true;
            });

            //https://weblog.west-wind.com/posts/2016/Sep/26/ASPNET-Core-and-CORS-Gotchas
            //https://docs.microsoft.com/en-us/aspnet/core/security/cors
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}
