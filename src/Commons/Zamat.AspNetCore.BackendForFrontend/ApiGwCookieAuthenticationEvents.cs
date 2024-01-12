using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class ApiGwCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<ApiGwCookieAuthenticationEvents> _logger;

    public ApiGwCookieAuthenticationEvents(IRefreshTokenService refreshTokenService, ILogger<ApiGwCookieAuthenticationEvents> logger)
    {
        _refreshTokenService = refreshTokenService;
        _logger = logger;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var refreshToken = context.Properties.GetTokenValue(Consts.RefreshToken)!;

        var expiresAt = context.Properties.GetTokenValue(Consts.ExpiresAt)!;

        if (!HasExpired(expiresAt))
        {
            return;
        }

        var result = await _refreshTokenService.TryRefreshTokenAsync(refreshToken, expiresAt, CancellationToken.None);

        if (result.IsError)
        {
            _logger.LogDebug("Unable to refresh token, reason: {refreshTokenErrorDescription}", result.ErrorDescription);

            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        context.Properties.UpdateTokenValue(Consts.IdToken, result.IdentityToken!);
        context.Properties.UpdateTokenValue(Consts.AccessToken, result.AccessToken!);
        context.Properties.UpdateTokenValue(Consts.RefreshToken, result.RefreshToken!);
        context.Properties.UpdateTokenValue(Consts.ExpiresAt, CalculateNewExpiresAt(result.ExpiresIn!));

        context.ShouldRenew = true;
    }

    private static string CalculateNewExpiresAt(int expiresIn)
    {
        return (DateTime.UtcNow + TimeSpan.FromSeconds(expiresIn)).ToString("o", CultureInfo.InvariantCulture);
    }

    private static bool HasExpired(string expiresAt)
    {
        return !DateTime.TryParse(expiresAt, out var expiresAtDate) || expiresAtDate < DateTime.Now;
    }
}
