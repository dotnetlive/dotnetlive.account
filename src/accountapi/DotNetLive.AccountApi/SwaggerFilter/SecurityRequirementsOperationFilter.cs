using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace DotNetLive.AccountApi
{
    // SecurityRequirementsOperationFilter.cs
    public class SecurityRequirementsOperationFilter : IOperationFilter, IDocumentFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            var isAllowAnonymous = filterPipeline.Select(f => f.Filter).Any(f => f is AllowAnonymousAttribute);
            if (!isAllowAnonymous)
                isAllowAnonymous = context.ApiDescription.ActionAttributes().Any(a => a is AllowAnonymousAttribute);
            if (isAllowAnonymous)
                return;

            var isAuthorized = filterPipeline.Select(f => f.Filter).Any(f => f is AuthorizeFilter);
            var authorizationRequired = filterPipeline.Select(f => f.Filter).Any(f => f is AuthorizeAttribute);

            //var authorizationRequired = context.ApiDescription.GetControllerAttributes().Any(a => a is AuthorizeAttribute);
            //if (!authorizationRequired) authorizationRequired = context.ApiDescription.GetActionAttributes().Any(a => a is AuthorizeAttribute);

            if (isAuthorized || authorizationRequired)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();

                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "JWT security token obtained from Identity Server.",
                    Required = true,
                    Type = "string"
                });

                // Policy names map to scopes
                //这里的Policy暂时还不知道是干啥用的
                var controllerScopes = context.ApiDescription.ControllerAttributes()
                    .OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy);

                var actionScopes = context.ApiDescription.ActionAttributes()
                    .OfType<AuthorizeAttribute>()
                    .Select(attr => attr.Policy);

                var requiredScopes = controllerScopes.Union(actionScopes).Distinct();

                //if (requiredScopes.Any())
                //{
                if (!operation.Responses.ContainsKey("401"))
                    operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                if (!operation.Responses.ContainsKey("403"))
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", requiredScopes }
                });
                //}

            }
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            IList<IDictionary<string, IEnumerable<string>>> security = swaggerDoc.SecurityDefinitions
                .Select(securityDefinition => new Dictionary<string, IEnumerable<string>>
                {
                    {securityDefinition.Key, new string[] {"yourapi"}}
                }).Cast<IDictionary<string, IEnumerable<string>>>().ToList();

            swaggerDoc.Security = security;
        }
    }
}
