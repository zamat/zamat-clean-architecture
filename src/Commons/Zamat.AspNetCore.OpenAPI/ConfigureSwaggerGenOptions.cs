using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zamat.AspNetCore.OpenAPI;

internal class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IOptions<OpenAPIOptions> openAPIConfig) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;
    private readonly IOptions<OpenAPIOptions> _openAPIConfig = openAPIConfig;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var apiVersion in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(apiVersion.GroupName, new OpenApiInfo()
            {
                Title = _openAPIConfig.Value.Title,
                Version = apiVersion.ApiVersion.ToString(),
                Description = _openAPIConfig.Value.Description,
                Contact = _openAPIConfig.Value.Contact,
                License = _openAPIConfig.Value.License
            });
        }
    }
}