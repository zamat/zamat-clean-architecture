using System.Reflection;
using MassTransit;
using MassTransit.Metadata;

namespace Zamat.AspNetCore.MassTransit;

// NOTE: Consumers have to be public https://github.com/MassTransit/MassTransit/issues/2253

public static class BusRegistrationConfiguratorExtensions
{
    /// <summary>
    /// Adds all consumers in the specified assemblies. Including private and internal types.
    /// </summary>
    /// <param name="configurator"></param>
    /// <param name="assemblies">The assemblies to scan for consumers.</param>
    public static IBusRegistrationConfigurator AddInternalConsumers(
        this IBusRegistrationConfigurator configurator,
        params Assembly[] assemblies
    )
    {
        foreach (var assembly in assemblies)
        {
            RegisterAssemblyCustomers(configurator, assembly);
        }

        return configurator;
    }

    internal static void RegisterAssemblyCustomers(
        IBusRegistrationConfigurator configurator,
        Assembly assembly
    )
    {
        configurator.AddConsumers(
            assembly.GetTypes().Where(RegistrationMetadata.IsConsumerOrDefinition).ToArray()
        );
    }
}
