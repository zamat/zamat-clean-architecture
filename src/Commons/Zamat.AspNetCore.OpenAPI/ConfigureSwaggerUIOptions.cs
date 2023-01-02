using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Zamat.AspNetCore.OpenAPI;

class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IOptions<OpenAPIOptions> _openAPIConfig;

    public ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider provider, IOptions<OpenAPIOptions> openAPIConfig)
    {
        _provider = provider;
        _openAPIConfig = openAPIConfig;
    }

    public void Configure(SwaggerUIOptions options)
    {
        foreach (var apiVersion in _provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{apiVersion.GroupName}/swagger.json", $"{_openAPIConfig.Value.Title} ({apiVersion.GroupName})");
        }
    }
}