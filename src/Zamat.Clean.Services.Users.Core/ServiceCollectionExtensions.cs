using Microsoft.Extensions.DependencyInjection;
using Zamat.Common.Command.Bus;
using Zamat.Common.DomainEventDispatcher;
using Zamat.Common.Query.Bus;
using Zamat.Clean.BuildingBlocks.Core;
using Zamat.Clean.Services.Users.Core.Domain;

namespace Zamat.Clean.Services.Users.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();

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
            o.AddDomainEventHandlers(UserCoreDomainAssembly.Assembly);
        });

        return services;
    }
}