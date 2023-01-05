using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Events.Bus.MassTransit;

class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEvent
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}
