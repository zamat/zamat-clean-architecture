using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Events.Bus;

public interface IEventHandler<T> where T : class, IEvent
{
    Task HandleAsync(T @event, CancellationToken cancellationToken = default);
}
