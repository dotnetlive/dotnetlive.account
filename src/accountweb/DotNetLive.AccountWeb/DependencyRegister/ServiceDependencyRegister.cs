using DotNetLive.AccountWeb.Configurations;
using DotNetLive.AccountWeb.Services;
using DotNetLive.Framework.DependencyManagement;
using DotNetLive.Framework.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetLive.AccountWeb.DependencyRegister
{
    public class ServiceDependencyRegister : IDependencyRegister
    {
        public ExecuteOrderType ExecuteOrder => ExecuteOrderType.Normal;

        public void Register(IServiceCollection services, IConfigurationRoot configuration, IServiceProvider serviceProvider)
        {
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

      
        }
    }
}
