using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zamat.AspNetCore.OpenAPI;

class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IOptions<OpenAPIOptions> _openAPIConfig;

    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IOptions<OpenAPIOptions> openAPIConfig)
    {
        _provider = provider;
        _openAPIConfig = openAPIConfig;
    }

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