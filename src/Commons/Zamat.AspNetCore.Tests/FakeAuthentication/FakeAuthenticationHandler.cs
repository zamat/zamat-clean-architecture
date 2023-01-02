using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

class FakeAuthenticationHandler : AuthenticationHandler<FakeAuthenticationSchemeOptions>
{
    internal const string AuthenticationType = "Test";

    public FakeAuthenticationHandler(IOptionsMonitor<FakeAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(Options.Claims, AuthenticationType);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties();

        var ticket = new AuthenticationTicket(principal, properties, AuthenticationType);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
