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

    public Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}
