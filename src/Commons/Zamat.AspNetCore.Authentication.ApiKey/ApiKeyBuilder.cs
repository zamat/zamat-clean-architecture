using AUMS.AspNetCore.Authentication.ApiKey.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.ApiKey;

public class ApiKeyBuilder
{
    private readonly IServiceCollection _services;

    public ApiKeyBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public ApiKeyBuilder UseStore<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where T : class, IApiKeyStore
    {
        _services.Add(ServiceDescriptor.Describe(typeof(IApiKeyStore), typeof(T), lifetime));
        AddApiKeyAuthentication();
        return this;
    }

    private void AddApiKeyAuthentication()
    {
        _services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                ApiKeyAuthenticationOptions.DefaultScheme,
                options => { }
            );
    }
}
