using Microsoft.Extensions.DependencyInjection;

namespace Zamat.AspNetCore.Grpc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureGrpcService(this IServiceCollection services)
    {
        services.AddGrpcHealthChecks();

        return services;
    }
}
