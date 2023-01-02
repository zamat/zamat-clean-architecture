using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Events.Bus;

public interface IEventBus
{
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}
