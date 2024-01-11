using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Command.Bus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandBus(this IServiceCollection services, Action<ICommandBusBuilder> setupAction)
    {
        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<ICommandBusRegistry, CommandBusRegistry>();
        var builder = new CommandBusBuilder(services);
        setupAction(builder);
        return services;
    }
}
