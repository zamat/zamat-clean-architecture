using Microsoft.AspNetCore.Http;

namespace Zamat.AspNetCore.Mvc.Rest.Middlewares;

public class RequestBufferingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestBufferingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        return _next(context);
    }
}
