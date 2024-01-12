using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AUMS.AspNetCore.BackendForFrontend;

public class AccessTokenDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly ILogger<AccessTokenDelegatingHandler> _logger;

    public AccessTokenDelegatingHandler(IHttpContextAccessor httpContextAccessor, ILogger<AccessTokenDelegatingHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");

        if (accessToken is null)
        {
            _logger.LogDebug("Access token not found in given httpContext.");

            return await base.SendAsync(request, cancellationToken);
        }

        _logger.LogDebug("Add access token to http request (accessToken: {accessToken})", accessToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
