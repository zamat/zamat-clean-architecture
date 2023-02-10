using Grpc.AspNetCore.Server;
using Zamat.AspNetCore.Grpc.Interceptors;

namespace Zamat.AspNetCore.Grpc;

public static class InterceptorCollectionExtensions
{
    public static InterceptorCollection AddLoggerInterceptor(this InterceptorCollection interceptors)
    {
        interceptors.Add<LoggerInterceptor>();
        return interceptors;
    }
}
