using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Zamat.AspNetCore.OpenAPI;

internal class ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider provider, IOptions<OpenAPIOptions> openAPIConfig) : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider _provider = provider;
    private readonly IOptions<OpenAPIOptions> _openAPIConfig = openAPIConfig;

    public void Configure(SwaggerUIOptions options)
    {
        foreach (var apiVersion in _provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{apiVersion.GroupName}/swagger.json", $"{_openAPIConfig.Value.Title} ({apiVersion.GroupName})");
        }
    }
}