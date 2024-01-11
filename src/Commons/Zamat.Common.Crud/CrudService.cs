using Zamat.Common.FilterQuery;

namespace Zamat.Common.Crud;

public abstract class CrudService<T, TIdentifier>(ICrudRepository<T, TIdentifier> repository)
{
    protected readonly ICrudRepository<T, TIdentifier> _repository = repository;

    internal enum Errors
    {
        ValidationError
    }

    public async Task<Result> CreateAsync(T entity, CancellationToken cancellationToken = default!)
    {
        var result = await ValidateAsync(entity, cancellationToken);
        if (!result.Succeeded)
        {
            return result;
        }

        await _repository.CreateAsync(entity, cancellationToken);
        return new Result();
    }

    public async Task<Result> UpdateAsync(TIdentifier id, Action<T> actionEntity, CancellationToken cancellationToken = default!)
    {
        var entity = await _repository.GetAsync(id, cancellationToken);
        if (entity is null)
        {
            return new Result(new PreconditionError(Errors.ValidationError, "Entity with given id not found"));
        }

        actionEntity(entity);

        var result = await ValidateAsync(entity, cancellationToken);
        if (!result.Succeeded)
        {
            return result;
        }

        await _repository.UpdateAsync(entity, cancellationToken);

        return new Result();
    }

    public async Task<Result> DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default!)
    {
        var entity = await _repository.GetAsync(id, cancellationToken);
        if (entity is null)
        {
            return new Result(new PreconditionError(Errors.ValidationError, "Entity with given id not found"));
        }

        await _repository.DeleteAsync(id, cancellationToken);

        return new Result();
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

    public abstract Task<Result> ValidateAsync(T entity, CancellationToken cancellationToken);
}
