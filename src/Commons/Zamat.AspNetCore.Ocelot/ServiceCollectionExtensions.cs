using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Zamat.AspNetCore.Ocelot;

namespace Zamat.AspNetCore.Ocelot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerForOcelot(this IServiceCollection services, IConfiguration configuration, Action<SwaggerDefinition> configureDefinition)
    {
        var definition = new SwaggerDefinition();
        configureDefinition(definition);

        services.AddSwaggerForOcelot(configuration, (opt) =>
        {
            opt.GenerateDocsForGatewayItSelf = true;
            opt.GenerateDocsForAggregates = true;
            opt.AggregateDocsGeneratorPostProcess = (aggregateRoute, routesDocs, pathItemDoc, documentation) =>
            {
                foreach (var param in definition.ParamsToRemove)
                {
                    pathItemDoc.RemoveParam(param);
                    documentation.RemoveParam(param);
                }
            };
        });

        return services;
    }
}