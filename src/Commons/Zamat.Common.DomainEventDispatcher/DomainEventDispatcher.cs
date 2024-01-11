using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Zamat.BuildingBlocks.Domain;

namespace Zamat.Common.DomainEventDispatcher;

internal class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> _handlerTypesCache = new();

    private static readonly ConcurrentDictionary<Type, Func<object, object, Task>> _handlersCache = new();

    private static readonly Type _handlerType = typeof(IDomainEventHandler<>);

    private static readonly Type EventHandlerFuncType = typeof(Func<Func<object, object, Task>>);
    private static readonly MethodInfo MakeDelegateMethod = typeof(DomainEventDispatcher).GetMethod(nameof(MakeDelegate), BindingFlags.Static | BindingFlags.NonPublic)!;

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        var handlerTypes = _handlerTypesCache.GetOrAdd(eventType, type => _handlerType.MakeGenericType(type));
        var eventHandlers = _serviceProvider.GetServices(handlerTypes);

        foreach (var eventHandler in eventHandlers)
        {
            var handlerServiceType = eventHandler!.GetType();

            var eventHandlerDelegate = _handlersCache.GetOrAdd(handlerServiceType, type =>
            {
                var makeDelegate = MakeDelegateMethod.MakeGenericMethod(eventType, type);

                return ((Func<Func<object, object, Task>>)makeDelegate
                    .CreateDelegate(EventHandlerFuncType))
                    .Invoke();
            });

            await eventHandlerDelegate(domainEvent, eventHandler);
        }
    }

    private static Func<object, object, Task> MakeDelegate<TEvent, TEventHandler>() where TEvent : class, IDomainEvent where TEventHandler : class, IDomainEventHandler<TEvent> => (domainEvent, eventHandler) => ((TEventHandler)eventHandler).HandleAsync((TEvent)domainEvent);
}
