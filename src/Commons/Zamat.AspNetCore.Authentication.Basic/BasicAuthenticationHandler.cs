using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using AUMS.AspNetCore.Authentication.Basic.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace AUMS.AspNetCore.Authentication.Basic;

internal class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    private readonly IBasicAuthenticator _authenticator;

    public BasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IBasicAuthenticator authenticator
    )
        : base(options, logger, encoder, clock)
    {
        _authenticator = authenticator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            Logger.LogDebug("No 'Authorization' header found in the request.");
            return AuthenticateResult.NoResult();
        }

        if (!AuthenticationHeaderValue.TryParse(Request.Headers[HeaderNames.Authorization], out var headerValue))
        {
            Logger.LogInformation("No valid 'Authorization' header found in the request.");
            return AuthenticateResult.NoResult();
        }

        if (!headerValue.Scheme.Equals(Consts.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
        {
            Logger.LogInformation($"'Authorization' header found but the scheme is not a '{Consts.AuthenticationScheme}' scheme.");
            return AuthenticateResult.NoResult();
        }

        BasicAuthenticationCredentials credentials;

        try
        {
            credentials = DecodeBase64(headerValue.Parameter!);
        }
        catch (Exception exception)
        {
            Logger.LogWarning(exception, "Error decoding credentials from header value.");

            return AuthenticateResult.Fail("Error decoding credentials from header value." + Environment.NewLine + exception.Message);
        }

        try
        {
            var result = await _authenticator.AuthenticateAsync(credentials);

            if (result.IsAuthenticated)
            {
                var identity = new ClaimsIdentity(result.Claims, Consts.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
            }

            return AuthenticateResult.Fail("Invalid username or password");

        }
        catch (Exception exception)
        {
            Logger.LogWarning(exception, "Error occured while authenticating basic auth schema.");
        }

        return AuthenticateResult.Fail("Invalid Authorization Header");
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{Options.Realm}\"");

        return base.HandleChallengeAsync(properties);
    }

    private static BasicAuthenticationCredentials DecodeBase64(string base64String)
    {
        var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(base64String));

        var credentials = credentialsAsEncodedString.Split(':');

        return new BasicAuthenticationCredentials(credentials[0], credentials[1]);
    }
}
