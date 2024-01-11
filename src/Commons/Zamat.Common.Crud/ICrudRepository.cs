using Zamat.Common.FilterQuery;

namespace Zamat.Common.Crud;

public interface ICrudRepository<T, TIdentifier>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default!);
    Task DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default!);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default!);
    Task<T?> GetAsync(TIdentifier id, CancellationToken cancellationToken = default!);
    Task<int> CountAsync(FilterQuery<T> filter, CancellationToken cancellationToken = default!);
    IAsyncEnumerable<T> GetAllAsync(FilterQuery<T> filter, int page, int limit, CancellationToken cancellationToken = default!);
    IAsyncEnumerable<T> GetAllAsync();
    Task<bool> CheckPredicateAsync(FilterQuery<T> filter, CancellationToken cancellationToken = default!);
}
