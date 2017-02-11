using DotNetLive.Framework.DependencyManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;

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
            services.AddSingleton<IServiceCollection>(factory => services);
            //services.AddSingleton<IContainer>(factory => ApplicationContainer);
            services.AddSingleton<IConfigurationRoot>(factory => Configuration);

            //先通过asp.net core ioc注册
            services.AddDependencyRegister(Configuration);
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DNL API V1", Version = "v1" });
                c.SwaggerDoc("v2", new Info { Title = "DNL API V2", Version = "v2" });
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DNL Accont API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "DNL Accont API V2");

                c.InjectStylesheet("/swagger.css");
                c.EnabledValidator();
                c.BooleanValues(new object[] { 0, 1 });
                c.DocExpansion("full");
                //c.InjectOnCompleteJavaScript("/swagger-ui/on-complete.js");
                //c.InjectOnFailureJavaScript("/swagger-ui/on-failure.js");
                c.SupportedSubmitMethods(new[] { "get", "post", "put", "patch" });
                c.ShowRequestHeaders();
                c.ShowJsonEditor();
            });

            //app.UseCookieAuthentication()

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
