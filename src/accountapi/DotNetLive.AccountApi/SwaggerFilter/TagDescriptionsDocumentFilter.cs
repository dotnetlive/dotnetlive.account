using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotNetLive.AccountApi
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new[] {
                new Tag { Name = "Products", Description = "Browse/manage the product catalog" },
                new Tag { Name = "Orders", Description = "Submit orders" }
            };
        }
    }
}
