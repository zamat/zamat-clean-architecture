using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;

namespace Zamat.Clean.ApiGateway.DelegatingHandlers;

class UsersDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<UsersDelegatingHandler> logger) : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<UsersDelegatingHandler> _logger = logger;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var routeHolder = _httpContextAccessor.HttpContext?.Items?.DownstreamRouteHolder();
        if (routeHolder is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var route = routeHolder.Route?.DownstreamRoute?.FirstOrDefault();
        if (route is null)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        if (route.Key is "Users")
        {
            DoSomethingWithRequest();
        }

        return await base.SendAsync(request, cancellationToken);
    }

    void DoSomethingWithRequest()
    {
        var sub = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? "notset";

        _logger.LogInformation("Processing {nameof} at utc {datetime} with auth context (sub: {sub})", nameof(UsersDelegatingHandler), DateTime.UtcNow.ToString("O"), sub);
    }
}

