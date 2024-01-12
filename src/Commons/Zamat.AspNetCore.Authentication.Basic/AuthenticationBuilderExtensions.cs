using Microsoft.AspNetCore.Authentication;

namespace AUMS.AspNetCore.Authentication.Basic;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddBasicAuthentication(this AuthenticationBuilder builder, Action<BasicAuthenticationBuilder> configure)
    {
        var basicAuthBuilder = new BasicAuthenticationBuilder(builder.Services);

        configure(basicAuthBuilder);

        return builder;
    }
}
