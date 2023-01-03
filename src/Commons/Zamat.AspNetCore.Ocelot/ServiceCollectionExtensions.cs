using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Zamat.AspNetCore.Ocelot;
using Zamat.AspNetCore.OpenAPI;

namespace Zamat.AspNetCore.Ocelot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerForOcelot(this IServiceCollection services, IConfiguration configuration, Action<SwaggerDefinitionOverride> configureDefinition)
    {
        var overrideDefinition = new SwaggerDefinitionOverride();
        configureDefinition(overrideDefinition);

        services.Configure(configureDefinition);

        services.AddSwaggerForOcelot(configuration, (opt) =>
        {
            opt.GenerateDocsForGatewayItSelf = true;
            opt.GenerateDocsForAggregates = true;
            opt.AggregateDocsGeneratorPostProcess = (aggregateRoute, routesDocs, pathItemDoc, documentation) =>
            {
                foreach (var param in overrideDefinition.ParamsToRemove)
                {
                    pathItemDoc.RemoveParam(param);
                    documentation.RemoveParam(param);
                }
            };
        });

        return services;
    }
}