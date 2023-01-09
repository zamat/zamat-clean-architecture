﻿using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Zamat.AspNetCore.Mvc.Rest;

class AddTraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public AddTraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Response.Headers.ContainsKey("X-Trace-Id"))
        {
            context.Response.Headers.Add("X-Trace-Id", Activity.Current?.Id ?? context.TraceIdentifier);
        }

        await _next(context);
    }
}
