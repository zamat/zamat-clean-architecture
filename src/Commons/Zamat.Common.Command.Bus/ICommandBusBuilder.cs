using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zamat.Common.Command.Bus;

public interface ICommandBusBuilder
{
    ICommandBusBuilder AddCommandHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped);
}
