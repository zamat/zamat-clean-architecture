using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

class FakeAuthenticationHandler(IOptionsMonitor<FakeAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : AuthenticationHandler<FakeAuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    internal const string AuthenticationType = "Test";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(Options.Claims, AuthenticationType);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties();

        var ticket = new AuthenticationTicket(principal, properties, AuthenticationType);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
