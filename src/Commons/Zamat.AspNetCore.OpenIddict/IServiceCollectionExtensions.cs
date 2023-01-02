using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation;
using System;

namespace Zamat.AspNetCore.OpenIddict;

public static class IServiceCollectionExtensions
{
    const string Schema = "OpenIddict.Validation.AspNetCore";

    public static IServiceCollection AddOpenIddictValidation(this IServiceCollection services, Action<OpenIddictValidationOptions> configureOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = Schema;
            options.DefaultChallengeScheme = Schema;
            options.DefaultForbidScheme = Schema;
            options.DefaultAuthenticateScheme = Schema;
        });

        services.AddOpenIddict().AddValidation(options =>
        {
            _ = options.Configure(configureOptions);
            _ = options.UseSystemNetHttp();
            _ = options.UseAspNetCore();
        });

        return services;
    }
}
