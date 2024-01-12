using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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

    public OpenTelemetryBuilder ConfigureTracing(Instrumentation instrumentation, Action<TracerProviderBuilder> configure, params string[] sources)
    {
        /*
        _serviceCollection.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(_resourceBuilder);

            builder.AddHttpClientInstrumentation();

            if (instrumentation.UseMassTransitInstrumentation)
            {
                builder.AddSource("MassTransit");
                builder.AddMassTransitInstrumentation();
            }

            if (instrumentation.UseEFCoreInstrumentation)
            {
                builder.AddEntityFrameworkCoreInstrumentation(x =>
                {
                    x.SetDbStatementForText = true;
                    x.SetDbStatementForStoredProcedure = true;
                });
            }

            if (instrumentation.UseRedisInstrumentation)
            {
                builder.AddRedisInstrumentation(instrumentation.ConnectionMultiplexer);
            }

            if (instrumentation.UseSqlClientInstrumentation)
            {
                builder.AddSqlClientInstrumentation(x =>
                {
                    x.SetDbStatementForText = true;
                    x.SetDbStatementForStoredProcedure = true;
                    x.RecordException = true;
                });
            }

            builder.AddSource(sources);

            configure(builder);
        });
        */
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
        /*
        _serviceCollection.AddOpenTelemetryMetrics(builder =>
        {
            builder.SetResourceBuilder(_resourceBuilder);
            builder.AddHttpClientInstrumentation();
            configure(builder);
        });
        */
        return this;
    }
}
