using Microsoft.Extensions.DependencyInjection;
using System;

namespace Zamat.Common.OpenTelemetry;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, OpenTelemetryServiceOptions serviceOptions, Action<OpenTelemetryBuilder> setupAction)
    {
        OpenTelemetryBuilder builder = new(services, serviceOptions);
        setupAction(builder);
        return services;
    }
}
