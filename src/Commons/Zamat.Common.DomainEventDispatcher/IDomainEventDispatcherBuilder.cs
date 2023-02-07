using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Zamat.Common.DomainEventDispatcher;

public interface IDomainEventDispatcherBuilder
{
    IDomainEventDispatcherBuilder AddDomainEventHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped);
}
