using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace DotNetLive.AccountApi
{
    // AuthResponsesOperationFilter.cs
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var authAttributes = context.ApiDescription
                .ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
        }
    }
}
