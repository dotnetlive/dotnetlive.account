using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;

namespace DotNetLive.AccountApi
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DNL API V1", Version = "v1" });
                c.SwaggerDoc("v2", new Info { Title = "DNL API V2", Version = "v2" });
                c.OperationFilter<AuthResponsesOperationFilter>();
                c.DocumentFilter<TagDescriptionsDocumentFilter>();

                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://petstore.swagger.io/oauth/dialog",
                    Scopes = new Dictionary<string, string>
                    {
                        { "readAccess", "Access read operations" },
                        { "writeAccess", "Access write operations" }
                    }
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseDeveloperExceptionPage();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseStaticFiles();

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

            ConfigureAuth(app);

            //// System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(byte[] rawData);
            //System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = DotNetUtilities.CreateX509Cert2("mycert");
            //Microsoft.IdentityModel.Tokens.SecurityKey secKey = new X509SecurityKey(cert2);

            //var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //{
            //    // The signing key must match!
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = signingKey,

            //    // Validate the JWT Issuer (iss) claim
            //    ValidateIssuer = true,
            //    ValidIssuer = "ExampleIssuer",

            //    // Validate the JWT Audience (aud) claim
            //    ValidateAudience = true,
            //    ValidAudience = "ExampleAudience",

            //    // Validate the token expiry
            //    ValidateLifetime = true,

            //    // If you want to allow a certain amount of clock drift, set that here:
            //    ClockSkew = TimeSpan.Zero,
            //};

            //var bearerOptions = new JwtBearerOptions()
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    TokenValidationParameters = tokenValidationParameters,
            //    Events = new CustomBearerEvents()
            //};

            //// Optional 
            //// bearerOptions.SecurityTokenValidators.Clear();
            //// bearerOptions.SecurityTokenValidators.Add(new MyTokenHandler());
            //app.UseJwtBearerAuthentication(bearerOptions);

            app.UseMvcWithDefaultRoute();
        }
    }
}
