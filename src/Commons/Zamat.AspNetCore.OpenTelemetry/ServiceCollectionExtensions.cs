using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System;
using Zamat.AspNetCore.OpenTelemetry;
using Zamat.Common.OpenTelemetry;

namespace Zamat.AspNetCore.OpenTelemetry;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry(o => configuration.GetSection(nameof(OpenTelemetryServiceOptions)).Bind(o));
        return services;
    }

    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, Action<OpenTelemetryServiceOptions> serviceOptions)
    {
        var opt = new OpenTelemetryServiceOptions();
        serviceOptions(opt);

        if (!opt.Enabled)
            return services;

        if (opt.OtlpEndpoint is null)
        {
            throw new InvalidOperationException("Otlp endpoint should be configured.");
        }

        var builder = services.AddOpenTelemetry(opt);

        builder.ConfigureTracing(opt, configure =>
        {
            configure.AddAspNetCoreInstrumentation();
            configure.AddOtlpExporter(configure =>
            {
                configure.Endpoint = opt.OtlpEndpoint;
            });
        });

        builder.ConfigureMetrics(configure =>
        {
            configure.AddAspNetCoreInstrumentation();
            configure.AddOtlpExporter(configure =>
            {
                configure.Endpoint = opt.OtlpEndpoint;
            });
        });

        builder.ConfigureLogging(_ => { });

        return services;
    }
}
