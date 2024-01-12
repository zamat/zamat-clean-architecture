using System;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using AUMS.AspNetCore.BackendForFrontend.EventHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace AUMS.AspNetCore.BackendForFrontend;

public static class WebApplicationExtensions
{
    private const string DefaultSchema = "BearerOrCookie";

    public static IServiceCollection AddBackendForFrontend(this IServiceCollection services, IConfiguration configuration)
    {
        var opt = new BackendForFrontendOptions();
        configuration.GetSection(nameof(BackendForFrontendOptions)).Bind(opt);

        services.Configure<BackendForFrontendOptions>(configuration.GetSection(nameof(BackendForFrontendOptions)));

        services
            .AddAuthorization()
            .AddAuthentication(options =>
        {
            options.DefaultScheme = DefaultSchema;
            options.DefaultChallengeScheme = DefaultSchema;
        })
        .AddCookie(cookie =>
        {
            cookie.EventsType = typeof(ApiGwCookieAuthenticationEvents);
            cookie.SlidingExpiration = true;
            cookie.Cookie.Name = opt.CookieName;
            cookie.Cookie.HttpOnly = false;
            cookie.Cookie.SameSite = opt.SameSiteMode;
            cookie.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            cookie.AccessDeniedPath = "/accessDenied";
            if (!string.IsNullOrEmpty(opt.CookieDomain))
            {
                cookie.Cookie.Domain = opt.CookieDomain;
            }
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = opt.Authority;

            if (!string.IsNullOrEmpty(opt.Audience))
            {
                options.Audience = opt.Audience;
            }

            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = !string.IsNullOrEmpty(opt.Audience),
            };
        })
        .AddOpenIdConnect(options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = opt.Authority;
            options.ClientId = opt.ClientId;
            options.UsePkce = true;
            options.ClientSecret = opt.ClientSecret;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.RequireHttpsMetadata = true;
            options.RemoteAuthenticationTimeout = opt.RemoteAuthenticationTimeout;

            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;

            options.SkipUnrecognizedRequests = true;

            options.Scope.Clear();

            var scopes = opt.Scopes;
            if (!string.IsNullOrEmpty(scopes))
            {
                var scopeArray = scopes.Split(" ");
                foreach (var scope in scopeArray)
                {
                    options.Scope.Add(scope);
                }
            }

            options.ClaimActions.MapAll();

            options.Events.OnAuthorizationCodeReceived = (context) => new AuthorizationCodeReceivedEventHandler().HandleAsync(context);
            options.Events.OnTokenValidated = (context) => new TokenValidatedEventHandler().HandleAsync(context);
            options.Events.OnRedirectToIdentityProvider = (context) => new RedirectToIdpEventHandler().HandleAsync(context);
            options.Events.OnRedirectToIdentityProviderForSignOut = (context) => new RedirectToIdpForSignOutEventHandler().HandleAsync(context);
            options.Events.OnUserInformationReceived = (context) => new UserInfoReceivedEventHandler().HandleAsync(context);
            options.Events.OnAuthenticationFailed = (context) => new AuthenticationFailedEventHandler().HandleAsync(context);
        })
        .AddPolicyScheme(DefaultSchema, DefaultSchema, options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                string authorization = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                {
                    return JwtBearerDefaults.AuthenticationScheme;
                }

                return CookieAuthenticationDefaults.AuthenticationScheme;
            };
        });

        services.AddScoped<ILoginHandler, DefaultLoginHandler>();
        services.AddScoped<ILogoutHandler, DefaultLogoutHandler>();
        services.AddScoped<IUserInfoHandler, DefaultUserInfoHandler>();
        services.AddScoped<IAccessDeniedHandler, DefaultAccessDeniedHandler>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<IRealmValidator, RealmValidator>();

        services.AddScoped<ApiGwCookieAuthenticationEvents>();

        services.AddSingleton<IPostConfigureOptions<CookieAuthenticationOptions>, ApiGwPostConfigureCookie>();

        return services;
    }

    public static WebApplication UseBackendForFrontend(this WebApplication app, Action<IEndpointRouteBuilder>? endpointsAction = null)
    {
        app.UseEndpoints(e =>
        {
            e.AddLoginEndpoint();
            e.AddLogoutEndpoint();
            e.AddUserInfoEndpoint();
            e.AddAccessDeniedEndpoint();
            if (endpointsAction is not null)
            {
                endpointsAction(e);
            }
        });

        return app;
    }
}
