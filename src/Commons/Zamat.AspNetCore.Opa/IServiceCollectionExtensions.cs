using System;
using System.Linq;
using AUMS.AspNetCore.Opa.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Opa;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOpa(this IServiceCollection services, Action<OpaOptions> options)
    {
        services.AddSingleton<IOpaEvaluator, OpaEvaluator>();
        services.AddSingleton<IAuthorizationPolicyProvider, OpaPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, OpaRequirementHandler>();
        services.Configure(options);

        services.AddHttpClient<OpaClient>();

        services.AddSingleton<IOpaPolicyStorage, OpaPolicyStorage>();

        return services;
    }

    public static IServiceCollection AddOpaStubs(this IServiceCollection services)
    {
        var opaPolicyEvalutator = services.SingleOrDefault(d => d.ServiceType == typeof(IOpaEvaluator));
        if (opaPolicyEvalutator is not null)
            services.Remove(opaPolicyEvalutator);
        services.AddSingleton<IOpaEvaluator, OpaEvaluatorStubs>();
        return services;
    }

    public static IServiceCollection AddOpal(this IServiceCollection services, Action<OpalOptions> options)
    {
        services.Configure(options);

        services.AddHttpClient<OpalClient>();
        services.AddScoped<IOpalEventPublisher, OpalClient>();

        return services;
    }
}
