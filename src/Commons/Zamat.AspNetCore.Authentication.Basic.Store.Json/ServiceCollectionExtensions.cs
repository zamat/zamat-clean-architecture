using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.Basic.Store.Json;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBasicAuthentication(this IServiceCollection services, IConfiguration configuration, string filePath)
    {
        services.Configure<JsonBasicAuthenticatorOptions>(opt =>
        {
            opt.FilePath = filePath;
        });

        services.AddBasicAuthentication(configuration, c =>
        {
            c.UseAuthenticator<JsonBasicAuthenticator>();
        });

        return services;
    }
}
