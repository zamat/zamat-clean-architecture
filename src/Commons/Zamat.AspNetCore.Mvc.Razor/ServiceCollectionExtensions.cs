using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Mvc.Razor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRazorViewToStringRenderer(this IServiceCollection services)
    {
        services.AddTransient<RazorViewToStringRenderer>();

        return services;
    }
}
