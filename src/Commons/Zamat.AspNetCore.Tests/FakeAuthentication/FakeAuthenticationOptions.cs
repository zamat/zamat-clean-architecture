using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

public class FakeAuthenticationOptions : IPostConfigureOptions<AuthenticationOptions>
{
    public void PostConfigure(string? name, AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = FakeAuthenticationHandler.AuthenticationType;
    }
}
