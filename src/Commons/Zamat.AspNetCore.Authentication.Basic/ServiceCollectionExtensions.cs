using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.Basic;

public static class ServiceCollectionExtensions
{
    public static BasicAuthenticationBuilder AddBasicAuthentication(this IServiceCollection services, IConfiguration configuration, Action<BasicAuthenticationBuilder> configure)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Consts.AuthenticationScheme;
                options.DefaultChallengeScheme = Consts.AuthenticationScheme;
            })
            .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(
                Consts.AuthenticationScheme,
                o => configuration.GetSection(nameof(BasicAuthenticationOptions)).Bind(o)
            );

        var builder = new BasicAuthenticationBuilder(services);
        configure(builder);

        return builder;
    }
}
