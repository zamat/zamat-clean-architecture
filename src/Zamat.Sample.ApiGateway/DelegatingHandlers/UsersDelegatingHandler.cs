using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Sample.ApiGateway.DelegatingHandlers;

class UsersDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UsersDelegatingHandler> _logger;

    public UsersDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<UsersDelegatingHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

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
        _logger.LogInformation("Processing {nameof} at utc {datetime}", nameof(UsersDelegatingHandler), DateTime.UtcNow.ToString("O"));
    }
}

