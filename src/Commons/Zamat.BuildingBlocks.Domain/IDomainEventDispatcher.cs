using System.Threading;
using System.Threading.Tasks;

namespace Zamat.BuildingBlocks.Domain;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
