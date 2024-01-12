using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.OpenTelemetry;

public static class ServiceCollectionExtensions
{
    public static OpenTelemetryBuilder ConfigureOpenTelemetry(this IServiceCollection services, OpenTelemetryServiceOptions serviceOptions)
    {
        OpenTelemetryBuilder builder = new(services, serviceOptions);
        return builder;
    }
}
