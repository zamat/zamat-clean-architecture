using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Ocelot.Middleware;

namespace AUMS.AspNetCore.Ocelot.ApiKey;

public class ApiKeyDelegatingHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ApiKeyDelegatingHandler> _logger;

    public ApiKeyDelegatingHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<ApiKeyDelegatingHandler> logger)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var routeHolder = _httpContextAccessor.HttpContext?.Items?.DownstreamRouteHolder();
        if (routeHolder is null)
        {
            return base.SendAsync(request, cancellationToken);
        }

        var route = routeHolder.Route?.DownstreamRoute?.FirstOrDefault();

        if (route is null)
        {
            return base.SendAsync(request, cancellationToken);
        }

        string serviceName = route.ServiceName;

        if (string.IsNullOrEmpty(serviceName))
        {
            return base.SendAsync(request, cancellationToken);
        }

        var apiKey = _configuration.GetValue<string>($"Services:{serviceName}:ApiKey");

        if (apiKey is null)
        {
            _logger.LogDebug("Api key not found.");
            return base.SendAsync(request, cancellationToken);
        }

        _logger.LogDebug("Add Api key to http request (ApiKey {ApiKey})", apiKey);
        request.Headers.Add("X-Api-Key", apiKey);
        return base.SendAsync(request, cancellationToken);
    }
}
