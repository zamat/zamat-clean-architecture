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
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration, Action<Instrumentation> instrumentationConfig)
    {
        var instrumentation = new Instrumentation();
        instrumentationConfig(instrumentation);

        var opt = new OpenTelemetryServiceOptions();
        configuration.GetSection(nameof(OpenTelemetryServiceOptions)).Bind(opt);

        services.AddOpenTelemetry(opt, instrumentation);
        return services;
    }

    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, OpenTelemetryServiceOptions serviceOptions, Instrumentation instrumentation)
    {
        if (!serviceOptions.Enabled)
            return services;

        if (serviceOptions.OtlpEndpoint is null)
        {
            throw new InvalidOperationException("Otlp endpoint should be configured.");
        }

        var builder = services.AddOpenTelemetry(serviceOptions);

        builder.ConfigureTracing(instrumentation, configure =>
        {
            configure.AddAspNetCoreInstrumentation();
            configure.AddOtlpExporter(configure =>
            {
                configure.Endpoint = serviceOptions.OtlpEndpoint;
            });
        });

        builder.ConfigureMetrics(configure =>
        {
            configure.AddAspNetCoreInstrumentation();
            configure.AddOtlpExporter(configure =>
            {
                configure.Endpoint = serviceOptions.OtlpEndpoint;
            });
        });

        builder.ConfigureLogging(_ => { });

        return services;
    }
}
