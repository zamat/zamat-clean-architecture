using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAuthentication(this IServiceCollection services, Action<FakeAuthenticationSchemeOptions>? options = null)
    {
        services.AddAuthentication(FakeAuthenticationHandler.AuthenticationType)
            .AddScheme<FakeAuthenticationSchemeOptions, FakeAuthenticationHandler>(FakeAuthenticationHandler.AuthenticationType, options);
        services.AddSingleton<IPostConfigureOptions<AuthenticationOptions>, FakeAuthenticationOptions>();
        return services;
    }
}
