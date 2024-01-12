using Grpc.AspNetCore.Server;
using Microsoft.Extensions.DependencyInjection;
using Zamat.AspNetCore.Grpc.Interceptors;

namespace Zamat.AspNetCore.Grpc;

public static class InterceptorCollectionExtensions
{
    public static InterceptorCollection AddLoggerInterceptor(this InterceptorCollection interceptors)
    {
        interceptors.Add<LoggerInterceptor>();
        return interceptors;
    }

    public static InterceptorCollection AddApiKeyInterceptor(this InterceptorCollection interceptors, string apiKey)
    {
        interceptors.Add<ApiKeyInterceptor>(apiKey);
        return interceptors;
    }

    public static IHttpClientBuilder AddApiKeyInterceptor(this IHttpClientBuilder clientBuilder, string apiKey)
    {
        clientBuilder.AddInterceptor(() => new ApiKeyInterceptor(apiKey));
        return clientBuilder;
    }
}
