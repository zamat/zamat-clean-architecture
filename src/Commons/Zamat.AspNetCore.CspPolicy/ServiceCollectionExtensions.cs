using System;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.CspPolicy;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCspOptions(this IServiceCollection services, Action<CspOptions> options)
    {
        services.Configure(options);

        return services;
    }
}
