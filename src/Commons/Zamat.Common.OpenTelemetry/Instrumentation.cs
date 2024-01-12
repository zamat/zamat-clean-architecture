using StackExchange.Redis;

namespace Zamat.Common.OpenTelemetry;
public class Instrumentation
{
    public bool UseMassTransitInstrumentation { get; private set; }

    public bool UseEFCoreInstrumentation { get; private set; } = false;

    public bool UseSqlClientInstrumentation { get; private set; } = false;

    public bool UseRedisInstrumentation { get; private set; } = false;

    internal IConnectionMultiplexer? ConnectionMultiplexer { get; private set; }

    public void AddMassTransitInstrumentation() => UseMassTransitInstrumentation = true;

    public void AddEFCoreInstrumentation() => UseEFCoreInstrumentation = true;

    public void AddRedisInstrumentation(IConnectionMultiplexer connectionMultiplexer)
    {
        UseRedisInstrumentation = true;
        ConnectionMultiplexer = connectionMultiplexer;
    }

    public void AddSqlClientInstrumentation() => UseSqlClientInstrumentation = true;
}
