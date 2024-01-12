using AUMS.AspNetCore.Authentication.Basic.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.Basic;

public class BasicAuthenticationBuilder
{
    private readonly IServiceCollection _services;

    public BasicAuthenticationBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public BasicAuthenticationBuilder UseAuthenticator<T>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where T : class, IBasicAuthenticator
    {
        _services.Add(ServiceDescriptor.Describe(typeof(IBasicAuthenticator), typeof(T), lifetime));
        return this;
    }
}
