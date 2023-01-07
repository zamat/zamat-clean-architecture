using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Events.Bus.MassTransit;

public abstract class EventHandler<T> : IEventHandler<T>, IConsumer<T> where T : class, IEvent
{
    public Task Consume(ConsumeContext<T> context)
    {
        var consumerMessage = context.Message;
        return HandleAsync(consumerMessage, context.CancellationToken);
    }

    public abstract Task HandleAsync(T @event, CancellationToken cancellationToken);
}
