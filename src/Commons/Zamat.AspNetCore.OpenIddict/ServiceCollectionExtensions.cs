using Microsoft.Extensions.DependencyInjection;

namespace Zamat.AspNetCore.OpenIddict;

public static class ServiceCollectionExtensions
{
    const string Schema = "OpenIddict.Validation.AspNetCore";

    public static IServiceCollection AddOidcAuthentication(this IServiceCollection services, Action<OidcAuthOptions> configureOptions)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultScheme = Schema;
            o.DefaultChallengeScheme = Schema;
            o.DefaultForbidScheme = Schema;
            o.DefaultAuthenticateScheme = Schema;
        });

        var opt = new OidcAuthOptions();
        configureOptions(opt);

        services.AddOpenIddict().AddValidation(o =>
        {
            o.UseAspNetCore();
            o.UseSystemNetHttp();

            o.SetIssuer(opt.Issuer);

            if (opt.Audiences is not null)
                o.AddAudiences(opt.Audiences);

            if (opt.UseIntrospection)
            {
                o.UseIntrospection();
                if (opt.ClientId is not null)
                    o.SetClientId(opt.ClientId);
                if (opt.ClientSecret is not null)
                    o.SetClientSecret(opt.ClientSecret);
            }
        });

        return services;
    }
}
