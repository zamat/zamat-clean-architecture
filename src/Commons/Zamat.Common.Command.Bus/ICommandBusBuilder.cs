using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.Command.Bus;

public interface ICommandBusBuilder
{
    ICommandBusBuilder AddCommandHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped);
}
