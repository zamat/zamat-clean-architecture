﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zamat.AspNetCore.Diagnostics;

class AddTraceIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Response.Headers.ContainsKey("X-Trace-Id"))
        {
            context.Response.Headers.Add("X-Trace-Id", Activity.Current?.Id ?? context.TraceIdentifier);
        }

        await _next(context);
    }
}
