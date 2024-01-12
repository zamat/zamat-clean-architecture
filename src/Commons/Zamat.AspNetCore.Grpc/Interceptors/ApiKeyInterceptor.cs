using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Zamat.AspNetCore.Grpc.Interceptors;

internal class ApiKeyInterceptor : Interceptor
{
    private readonly string _apiKey;

    public ApiKeyInterceptor(string apiKey)
    {
        _apiKey = apiKey;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var metadata = new Metadata
        {
            { "X-Api-Key", _apiKey }
        };
        var options = context.Options.WithHeaders(metadata);
        var updatedContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
        return continuation(request, updatedContext);
    }
}
