using Zamat.Clean.Services.Audit.Worker.Core.EventHandlers;
using Zamat.Common.Events.Bus;
using Zamat.Clean.BuildingBlocks.Core;
using Zamat.Clean.Services.Users.Core.IntegrationEvents;

namespace Zamat.Clean.Services.Audit.Worker.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();
        services.AddScoped<IEventHandler<UserCreated>, UserCreatedEventHandler>();

        return services;
    }
}