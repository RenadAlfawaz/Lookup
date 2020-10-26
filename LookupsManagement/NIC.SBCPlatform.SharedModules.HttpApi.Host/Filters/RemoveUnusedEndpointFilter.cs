using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace NIC.SBCPlatform.SharedModules.Filters
{
    public class RemoveUnusedEndpointFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.RelativePath.Contains("/abp/"))
                {
                    var keyToRemove = swaggerDoc.Paths
                        .FirstOrDefault(p => p.Key.ToLower().Contains(apiDescription.RelativePath.ToLower())).Key;

                    if (keyToRemove != null)
                        swaggerDoc.Paths.Remove(keyToRemove);
                }

            }
        }
    }
}
