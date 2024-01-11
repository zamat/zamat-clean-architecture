using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.AspNetCore.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder ConfigureExternalProviders(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        var providers = new AuthenticationProviders();
        configuration.GetSection(nameof(AuthenticationProviders)).Bind(providers);

        if (providers.Google is not null)
        {
            builder.AddGoogle(googleOptions =>
             {
                 googleOptions.ClientId = providers.Google.ClientId;
                 googleOptions.ClientSecret = providers.Google.ClientSecret;
             });
        }
        if (providers.Twitter is not null)
        {
            builder.AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = providers.Twitter.ConsumerKey;
                twitterOptions.ConsumerSecret = providers.Twitter.ConsumerSecret;
            });
        }
        if (providers.OpenIdConnect is not null)
        {
            var openIdConnectProvider = providers.OpenIdConnect;
            builder.AddOpenIdConnect("OpenIdConnect", openIdConnectProvider.DisplayName, options =>
            {
                if (!string.IsNullOrEmpty(openIdConnectProvider.Authority))
                {
                    options.Authority = openIdConnectProvider.Authority;
                }
                options.ClientId = openIdConnectProvider.ClientId;
                options.ClientSecret = openIdConnectProvider.ClientSecret;
                options.ResponseType = openIdConnectProvider.ResponseType;
                options.GetClaimsFromUserInfoEndpoint = openIdConnectProvider.GetClaimsFromUserInfoEndpoint;
                options.SaveTokens = openIdConnectProvider.SaveTokens;
                options.Scope.Clear();

                if (!string.IsNullOrEmpty(openIdConnectProvider.Scopes))
                {
                    foreach (var scope in openIdConnectProvider.Scopes.Split(" "))
                        options.Scope.Add(scope);
                }

                if (openIdConnectProvider.TokenValidationParameters is not null)
                {
                    options.TokenValidationParameters = openIdConnectProvider.TokenValidationParameters;
                }
                if (openIdConnectProvider.Configuration is not null)
                {
                    options.Configuration = openIdConnectProvider.Configuration;
                }

                options.Events.OnUserInformationReceived = (ctx) =>
                {
                    return Task.CompletedTask;
                };
                options.Events.OnTokenValidated = (ctx) =>
                {
                    return Task.CompletedTask;
                };
                options.Events.OnTokenResponseReceived = (ctx) =>
                {
                    return Task.CompletedTask;
                };
            });
        }
        return builder;
    }

    class AuthenticationProviders
    {
        public GoogleOptions? Google { get; set; }
        public TwitterOptions? Twitter { get; set; }
        public CustomOpenIdConnectOptions? OpenIdConnect { get; set; }
    }

    class CustomOpenIdConnectOptions : OpenIdConnectOptions
    {
        public string? DisplayName { get; set; }
        public string Scopes { get; set; } = "oidc email";
    }
}
