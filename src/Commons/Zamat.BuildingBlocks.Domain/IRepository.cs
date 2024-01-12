namespace Zamat.BuildingBlocks.Domain;

public interface IRepository<T, TKey> where T : class, IAggregateRoot
{
    Task<T> GetAsync(TKey id, CancellationToken cancellationToken);

    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(T entity, CancellationToken cancellationToken);
}
