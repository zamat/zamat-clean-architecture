using Zamat.Sample.Services.Users.Api.Rest.ProblemDetails;

namespace Zamat.Sample.Services.Users.Api.Rest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProblemFactory(this IServiceCollection services)
    {
        services.AddScoped<IProblemFactory, ProblemFactory>();

        return services;
    }
}