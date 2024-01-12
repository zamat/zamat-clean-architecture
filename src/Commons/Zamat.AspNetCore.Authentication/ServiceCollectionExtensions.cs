using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Zamat.AspNetCore.Authentication;

public static class ServiceCollectionExtensions
{
    internal const string Schema = "Bearer";

    public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultScheme = Schema;
            o.DefaultChallengeScheme = Schema;
            o.DefaultForbidScheme = Schema;
            o.DefaultAuthenticateScheme = Schema;
        }).AddBearer(configuration);

        return services;
    }

    public static IServiceCollection AddAccessTokenQueryResolver(this IServiceCollection services)
    {
        services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, GetAccessTokenFromQueryPostConfigure>();

        return services;
    }
}
