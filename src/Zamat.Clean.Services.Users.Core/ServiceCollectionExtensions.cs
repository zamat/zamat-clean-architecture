using Microsoft.Extensions.DependencyInjection;
using Zamat.Clean.Core;
using Zamat.Clean.Services.Users.Core.Domain;
using Zamat.Common.Command.Bus;
using Zamat.Common.DomainEventDispatcher;
using Zamat.Common.Query.Bus;

namespace Zamat.Clean.Services.Users.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreCommon();

        services.AddQueryBus(o =>
        {
            o.AddQueryHandlers(UserCoreAssembly.Assembly);
        });

        services.AddCommandBus(o =>
        {
            o.AddCommandHandlers(UserCoreAssembly.Assembly);
        });

        services.AddDomainEventDispatcher(o =>
        {
            o.AddDomainEventHandlers(DomainAssembly.Assembly);
        });

        return services;
    }
}