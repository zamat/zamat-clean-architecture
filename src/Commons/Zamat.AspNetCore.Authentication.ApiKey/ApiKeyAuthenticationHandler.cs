using System.Security.Claims;
using System.Text.Encodings.Web;
using AUMS.AspNetCore.Authentication.ApiKey.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.Authentication.ApiKey;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private readonly IApiKeyStore _authRepository;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IApiKeyStore authRepository
    )
        : base(options, logger, encoder, clock)
    {
        _authRepository = authRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("X-Api-Key"))
        {
            // No API Key was supplied
            return AuthenticateResult.Fail("API Key was not provided");
        }

        string providedApiKey = Request.Headers["X-Api-Key"];
        var apiKey = await _authRepository.GetApiKeyAsync(providedApiKey);

        if (apiKey == null)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        var identity = new ClaimsIdentity(apiKey.Claims, Options.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Options.Scheme);

        return AuthenticateResult.Success(ticket);
    }
}
