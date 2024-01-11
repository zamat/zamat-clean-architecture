using Ocelot.Middleware;

namespace Zamat.Clean.Api.Gateway.DelegatingHandlers;

internal class UsersDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<UsersDelegatingHandler> logger) : DelegatingHandler
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

    private void DoSomethingWithRequest()
    {
        var sub = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? "notset";

        _logger.LogInformation("Processing {nameof} at utc {datetime} with auth context (sub: {sub})", nameof(UsersDelegatingHandler), DateTime.UtcNow.ToString("O"), sub);
    }
}

