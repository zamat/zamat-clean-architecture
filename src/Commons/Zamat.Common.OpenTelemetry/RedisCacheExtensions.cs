using System.Reflection;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

namespace Zamat.Common.OpenTelemetry;

public static class RedisCacheExtensions
{
    public static ConnectionMultiplexer GetConnection(this RedisCache cache)
    {
        typeof(RedisCache).InvokeMember("Connect", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, cache, Array.Empty<object>());
        var fieldInfo = typeof(RedisCache).GetField("_connection", BindingFlags.Instance | BindingFlags.NonPublic);
        var connection = (ConnectionMultiplexer)fieldInfo?.GetValue(cache)!;
        return connection;
    }
}
