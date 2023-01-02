using System.Threading;
using System.Threading.Tasks;

namespace Zamat.BuildingBlocks.Domain;

public interface IDomainEventHandler<T> where T : class, IDomainEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);
}
