using DotNetLive.Framework.DependencyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetLive.Account.Services.DependencyRegister
{
    public class ServiceDependencyRegister : IDependencyRegister
    {
        ExecuteOrderType IDependencyRegister.ExecuteOrder =>  ExecuteOrderType.Normal ;
        public void Register(IServiceCollection services, IConfigurationRoot configuration, IServiceProvider serviceProvider)
        {
            services.AddScoped<UserCommandService>();
            services.AddScoped<UserQueryService>();
            services.AddScoped<AccountService>();
            services.AddScoped<UserDeviceCommandService>();
            services.AddScoped<UserDeviceQueryService>();
        }
    }
}
