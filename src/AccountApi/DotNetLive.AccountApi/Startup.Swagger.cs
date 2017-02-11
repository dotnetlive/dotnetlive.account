using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetLive.AccountApi
{
    public partial class Startup
    {
        private void ConfigSwagger(IServiceCollection services)
        {
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

        private void ConfigSwagger(IApplicationBuilder app)
        {
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
        }
    }
}
