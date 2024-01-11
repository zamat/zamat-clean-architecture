using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Zamat.AspNetCore.Grpc.Interceptors;

internal class LoggerInterceptor(ILogger<LoggerInterceptor> logger) : Interceptor
{
    private readonly ILogger<LoggerInterceptor> _logger = logger;

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Grpc server receive call (type: {type}, method: {method}).", MethodType.Unary, context.Method);

        return await continuation(request, context);
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Starting grpc client call (type: {type}, method: {method}).", context.Method.Type, context.Method.Name);

        return continuation(request, context);
    }
}