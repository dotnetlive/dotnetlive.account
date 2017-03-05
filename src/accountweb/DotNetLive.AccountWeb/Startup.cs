using DotNetLive.Framework.DependencyManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using NLog.Web;
using NLog.Extensions.Logging;
using NLog;
using Npgsql.Logging;

namespace DotNetLive.AccountWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
            services.AddSingleton(factory => services);
            services.AddSingleton(factory => Configuration);

            services.AddScoped<LogFilter>();

            services.AddMemoryCache();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.CookieName = ".dnl.session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            //先通过asp.net core ioc注册
            services.AddDependencyRegister(Configuration);

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory = NpgsqlLogManager.LoggerFactory;
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();
            ////foreach (DatabaseTarget target in LogManager.Configuration.AllTargets.Where(t => t is DatabaseTarget))
            ////{
            ////    target.ConnectionString = Configuration.GetConnectionString("NLogDb");
            ////}

            ////LogManager.ReconfigExistingLoggers();

            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("NLogDb");

            app.UseSession();

            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class LogFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LogFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("OnActionExecuting");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
