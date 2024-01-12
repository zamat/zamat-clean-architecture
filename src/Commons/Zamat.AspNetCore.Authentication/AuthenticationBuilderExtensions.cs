using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamat.AspNetCore.Authentication;

namespace Zamat.AspNetCore.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddBearer(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        builder.AddJwtBearer(o =>
        {
            configuration.GetSection(nameof(JwtBearerOptions)).Bind(o);
        });

        return builder;
    }

    public static AuthenticationBuilder AddBearer(this AuthenticationBuilder builder, Action<JwtBearerOptions> configureOptions)
    {
        builder.AddJwtBearer(configureOptions);

        return builder;
    }

    public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        builder.AddGoogle(o =>
        {
            configuration.GetSection(nameof(GoogleOptions)).Bind(o);
        });

        return builder;
    }

    public static AuthenticationBuilder AddTwitter(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        builder.AddTwitter(o =>
        {
            configuration.GetSection(nameof(TwitterOptions)).Bind(o);
        });

        return builder;
    }

    public static AuthenticationBuilder AddOpenIdConnect(this AuthenticationBuilder builder, string displayName, IConfiguration configuration)
    {
        builder.AddOpenIdConnect("OpenIdConnect", displayName, o =>
        {
            configuration.GetSection(nameof(OpenIdConnectOptions)).Bind(o);
        });

        return builder;
    }
}
