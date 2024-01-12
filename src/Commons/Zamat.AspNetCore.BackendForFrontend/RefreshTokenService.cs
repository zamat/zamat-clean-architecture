using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class RefreshTokenService : IRefreshTokenService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RefreshTokenService> _logger;
    private readonly IOptions<BackendForFrontendOptions> _options;

    public RefreshTokenService(HttpClient httpClient, ILogger<RefreshTokenService> logger, IOptions<BackendForFrontendOptions> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options;
    }

    public async Task<TokenResponse> TryRefreshTokenAsync(string refreshToken, string expiresAt, CancellationToken cancellationToken)
    {
        var bffOptions = _options.Value;

        var discovery = await _httpClient.GetDiscoveryDocumentAsync(bffOptions.Authority, cancellationToken: cancellationToken);
        if (discovery.IsError)
        {
            throw new Exception(discovery.Error);
        }

        _logger.LogDebug("Sending refresh token request");

        return await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
        {
            Address = discovery.TokenEndpoint,
            RefreshToken = refreshToken,
            Scope = bffOptions.Scopes,
            ClientId = bffOptions.ClientId,
            ClientSecret = bffOptions.ClientSecret
        },
        cancellationToken);
    }
}
