namespace Zamat.Common.Crud;

public interface ICrudRepository<T, TIdentifier> : IReadRepository<T, TIdentifier>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default!);

    Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default!);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default!);
}
