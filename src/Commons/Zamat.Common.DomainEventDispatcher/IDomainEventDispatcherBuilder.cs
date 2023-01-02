using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zamat.Common.DomainEventDispatcher;

public interface IDomainEventDispatcherBuilder
{
    IDomainEventDispatcherBuilder AddDomainEventHandlers(Assembly assembly, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped);
}
