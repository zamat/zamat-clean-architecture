using Zamat.Common.Events.Bus;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Audit.Worker.Core.EventHandlers;
using Zamat.Sample.Services.Users.Core.IntegrationEvents;

namespace Zamat.Sample.Services.Audit.Worker.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddCoreBuildingBlocks();
        services.AddScoped<IEventHandler<UserCreated>, UserCreatedEventHandler>();

        return services;
    }
}