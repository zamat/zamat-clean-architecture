using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Authorization;
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
            if (overrideDefinition.GatewaySwaggerGen is not null)
            {
                opt.GenerateDocsDocsForGatewayItSelf(overrideDefinition.GatewaySwaggerGen);
            }

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

    public static IServiceCollection DecorateClaimAuthorizer(this IServiceCollection services)
    {
        var serviceDescriptor = services.First(x => x.ServiceType == typeof(IClaimsAuthorizer));
        services.Remove(serviceDescriptor);

        var newServiceDescriptor = new ServiceDescriptor(serviceDescriptor.ImplementationType!, serviceDescriptor.ImplementationType!, serviceDescriptor.Lifetime);
        services.Add(newServiceDescriptor);

        services.AddTransient<IClaimsAuthorizer, ClaimAuthorizerDecorator>();

        return services;
    }
}
