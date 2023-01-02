using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Zamat.AspNetCore.OpenAPI.Attributes;

namespace Zamat.AspNetCore.OpenAPI.Filters;

class SwaggerIgnoreFilter : IOperationFilter, ISchemaFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parametersToRemove = operation.Parameters
            .Where(parameter => context.ApiDescription.ParameterDescriptions.Any(e => string.Compare(e.Name, parameter.Name, true) == 0 && e.CustomAttributes().OfType<SwaggerIgnoreAttribute>().Any()))
            .ToList();
        parametersToRemove.ForEach(parameter => operation.Parameters.Remove(parameter));
        parametersToRemove.Clear();
    }
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var propertiesToRemove = schema.Properties
            .Where(property =>
                context.Type.GetProperties().Any(e => e.Name == property.Key && e.GetCustomAttributes().OfType<SwaggerIgnoreAttribute>().Any()) ||
                context.Type.GetFields().Any(e => e.Name == property.Key && e.GetCustomAttributes().OfType<SwaggerIgnoreAttribute>().Any()) ||
                context.Type.GetMembers().Any(e => e.Name == property.Key && e.GetCustomAttributes().OfType<SwaggerIgnoreAttribute>().Any())
            )
            .Select(property => property.Key)
            .ToList();
        propertiesToRemove.ForEach(propertyKey => schema.Properties.Remove(propertyKey));
        propertiesToRemove.Clear();
    }
}