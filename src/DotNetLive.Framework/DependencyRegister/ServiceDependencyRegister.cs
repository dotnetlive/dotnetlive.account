using DotNetLive.Framework.Data;
using DotNetLive.Framework.Data.Repositories;
using DotNetLive.Framework.DependencyManagement;
using DotNetLive.Framework.Models;
using DotNetLive.Framework.UserIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            #endregion

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
