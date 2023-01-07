using Microsoft.Extensions.DependencyInjection;
using Zamat.Common.Command.Bus;
using Zamat.Common.DomainEventDispatcher;
using Zamat.Common.Events.Bus;
using Zamat.Common.Query.Bus;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Users.Api.Rest.IntegrationEvents;
using Zamat.Sample.Services.Users.Core.Domain;

namespace Zamat.Sample.Services.Users.Core;

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

        services.AddTransient<IEventHandler<UserCreated>, UserCreatedEventHandler>();

        return services;
    }
}