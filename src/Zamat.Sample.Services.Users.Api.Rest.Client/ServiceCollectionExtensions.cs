using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Zamat.Sample.Services.Users.Api.Rest.Client.V1;

namespace Zamat.Sample.Services.Users.Api.Rest.Client;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddUsersClient(this IServiceCollection services, string baseUrl, Action<HttpClient>? configure = null)
    {
        var builder = services.AddHttpClient<IUsersClient, UsersClient>(
            (provider, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
                if (configure is not null)
                    configure(client);
            });
        return builder;
    }
}