using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zamat.AspNetCore.Authorization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services, bool fallback = false, Action<AuthorizationOptions>? configureOptions = null)
    {
        services.AddAuthorization(options =>
        {
            if (fallback)
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            }
            if (configureOptions is not null)
            {
                configureOptions(options);
            }
        });

        return services;
    }
}
