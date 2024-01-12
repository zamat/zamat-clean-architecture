using Zamat.Common.FilterQuery;

namespace Zamat.Common.Crud;

public interface IReadRepository<T, TIdentifier>
{
    Task<T?> GetAsync(TIdentifier id, CancellationToken cancellationToken = default!);

    Task<int> CountAsync(FilterQuery<T> filter, CancellationToken cancellationToken = default!);

    IAsyncEnumerable<T> GetAllAsync(FilterQuery<T> filter, int page, int limit, CancellationToken cancellationToken = default!);

    IAsyncEnumerable<T> GetAllAsync();

    Task<bool> CheckPredicateAsync(FilterQuery<T> filter, CancellationToken cancellationToken = default!);
}
