namespace Zamat.BuildingBlocks.Domain;

public interface IRepository<T, TKey> where T : class, IAggregateRoot
{
    Task<T?> GetAsync(TKey id, CancellationToken cancellationToken);
    Task AddAsync(T user, CancellationToken cancellationToken);
    Task DeleteAsync(T user, CancellationToken cancellationToken);
}
