namespace Zamat.Common.Events.Bus;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
}
