using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Zamat.AspNetCore.OpenAPI.Filters;

namespace Zamat.AspNetCore.OpenAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiDoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApiDoc(o => configuration.GetSection(nameof(OpenAPIOptions)).Bind(o));
        return services;
    }

    public static IServiceCollection AddOpenApiDoc(this IServiceCollection services, Action<OpenAPIOptions> options)
    {
        services.Configure(options);

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>();

        services.AddSwaggerGen(opt =>
        {
            opt.OperationFilter<SwaggerIgnoreFilter>();
            opt.SchemaFilter<SwaggerIgnoreFilter>();
            opt.AddEnumsWithValuesFixFilters();
            opt.EnableAnnotations();
        });

        services.AddApiVersioning();

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        return services;
    }
}