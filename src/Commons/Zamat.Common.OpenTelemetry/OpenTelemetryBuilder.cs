using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using Zamat.Common.OpenTelemetry;

namespace Zamat.Common.OpenTelemetry;

public class OpenTelemetryBuilder
{
    private readonly IServiceCollection _serviceCollection;
    private readonly ResourceBuilder _resourceBuilder;

    public OpenTelemetryBuilder(IServiceCollection serviceCollection, OpenTelemetryServiceOptions serviceOptions)
    {
        _serviceCollection = serviceCollection;
        _resourceBuilder = ResourceBuilder.CreateDefault();
        if (serviceOptions is not null)
        {
            _resourceBuilder.AddService(
                serviceOptions.ServiceName,
                serviceOptions.ServiceNamespace,
                serviceOptions.ServiceVersion,
                serviceOptions.AutoGenerateServiceInstanceId,
                serviceOptions.ServiceInstanceId);
        }
    }

    public OpenTelemetryBuilder ConfigureTracing(Action<TracerProviderBuilder> configure, params string[] sources)
    {
        _serviceCollection.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(_resourceBuilder);
            builder.AddHttpClientInstrumentation();
            builder.AddAspNetCoreInstrumentation();
            builder.AddSource(sources);
            configure(builder);
        });
        return this;
    }

    public OpenTelemetryBuilder ConfigureLogging(Action<OpenTelemetryLoggerOptions> configure)
    {
        _serviceCollection.AddLogging(logging =>
        {
            logging.AddOpenTelemetry(builder =>
            {
                builder.SetResourceBuilder(_resourceBuilder);
                builder.IncludeScopes = true;
                builder.ParseStateValues = true;
                builder.IncludeFormattedMessage = true;
                configure(builder);
            });
        });
        return this;
    }

    public OpenTelemetryBuilder ConfigureMetrics(Action<MeterProviderBuilder> configure)
    {
        _serviceCollection.AddOpenTelemetryMetrics(builder =>
        {
            builder.SetResourceBuilder(_resourceBuilder);
            builder.AddHttpClientInstrumentation();
            builder.AddAspNetCoreInstrumentation();
            configure(builder);
        });
        return this;
    }
}
