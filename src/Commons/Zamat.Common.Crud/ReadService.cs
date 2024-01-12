using Zamat.Common.FilterQuery;

namespace Zamat.Common.Crud;

public abstract class ReadService<T, TIdentifier>
{
    private readonly IReadRepository<T, TIdentifier> _repository;

    public ReadService(IReadRepository<T, TIdentifier> repository)
    {
        _repository = repository;
    }

    public Task<int> CountAsync(FilterQuery<T> filter, CancellationToken cancellationToken = default!)
    {
        return _repository.CountAsync(filter, cancellationToken);
    }

    public IAsyncEnumerable<T> GetAsync(FilterQuery<T> filter, int page, int limit)
    {
        return _repository.GetAllAsync(filter, page, limit);
    }

    public Task<T?> GetAsync(TIdentifier id, CancellationToken cancellationToken = default!)
    {
        return _repository.GetAsync(id, cancellationToken);
    }
}
