using DotNetLive.AccountWeb.Configurations;
using DotNetLive.Framework.Data;
using DotNetLive.Framework.Data.Repositories;
using DotNetLive.AccountWeb.Services;
using DotNetLive.Framework.UserIdentity;
using DotNetLive.Framework.DependencyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DotNetLive.Framework.DependencyRegister
{
    public class ServiceDependencyRegister : IDependencyRegister
    {
        public void Register(IServiceCollection services, IConfigurationRoot configuration, IServiceProvider serviceProvider)
        {
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            #region Data Layer
            services.Configure<DbSettings>(configuration.GetSection("DbSettings"));

            services.AddScoped<CommandDbConnection>();
            services.AddScoped<QueryDbConnection>();

            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<ICommandRepository, CommandRepository>();
            #endregion

            #region UserIdentity Module Part
            services.AddScoped<IAuthenticationCommandAppService, AuthenticationCommandAppService>();
            services.AddScoped<IAuthenticationQueryAppService, AuthenticationQueryAppService>();
            #endregion

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }
    }
}
