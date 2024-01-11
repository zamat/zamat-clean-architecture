namespace Zamat.BuildingBlocks.Domain;

public interface IDomainEventHandler<T> where T : class, IDomainEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);
}
