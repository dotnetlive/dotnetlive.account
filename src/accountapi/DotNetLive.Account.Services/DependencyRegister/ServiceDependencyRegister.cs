using DotNetLive.Framework.DependencyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetLive.Account.Services.DependencyRegister
{
    public class ServiceDependencyRegister : IDependencyRegister
    {
        public ExecuteOrderType ExecuteOrder =>  ExecuteOrderType.Normal;

        public void Register(IServiceCollection services, IConfigurationRoot configuration, IServiceProvider serviceProvider)
        {
            services.AddScoped<UserCommandService>();
            services.AddScoped<UserQueryService>();
        }
    }
}
