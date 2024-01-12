using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Zamat.AspNetCore.OpenAPI;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
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

        if (_openAPIConfig.Value.AuthenticationScheme == "Bearer")
        {
            AddOpenApiBearerScheme(options);
        }
    }

    internal static void AddOpenApiBearerScheme(SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "OpenID Connect",
            Name = "OpenID Connect Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            Description = "Paste your OpenID Connect (OIDC) token below",
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(
            jwtSecurityScheme.Reference.Id,
            jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                jwtSecurityScheme, Array.Empty<string>()
            }
        });
    }
}
