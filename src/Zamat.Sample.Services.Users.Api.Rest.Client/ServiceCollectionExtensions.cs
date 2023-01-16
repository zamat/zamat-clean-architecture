using Microsoft.Extensions.DependencyInjection;
using System;
using Zamat.Sample.Services.Users.Api.Rest.Client.V1;

namespace Zamat.Sample.Services.Users.Api.Rest.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersClient(this IServiceCollection services, string baseUrl)
    {
        services.AddHttpClient<IUsersClient, UsersClient>(
            (provider, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });
        return services;
    }
}