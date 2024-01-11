using System;
using Microsoft.Extensions.DependencyInjection;
using Users.Api.V1;

namespace Zamat.Clean.Services.Users.Api.Grpc.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersGrpcClient(this IServiceCollection services, string serviceUrl)
    {
        services.AddGrpcClient<UsersSvc.UsersSvcClient>(o =>
        {
            o.Address = new Uri(serviceUrl);
        })
        .EnableCallContextPropagation();

        return services;
    }
}