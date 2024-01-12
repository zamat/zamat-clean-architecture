using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AUMS.Common.Cache;

internal sealed class LocalCache : ILocalCache
{
    private readonly ConcurrentDictionary<string, LocalCacheEntry> _cache = new();

    /// <inheritdoc />
    public T? Get<T>(string key)
        where T : class
    {
        if (!_cache.TryGetValue(key, out LocalCacheEntry? cacheEntry))
        {
            return default(T?);
        }

        if (cacheEntry.Value is not T cacheValue)
        {
            throw new InvalidCastException(
                $"Unable to cast the object of type {cacheEntry.Value.GetType()} to {typeof(T)}"
            );
        }

        if (cacheEntry.IsExpired())
        {
            _cache.TryRemove(key, out _);

            return null;
        }

        return cacheValue;
    }

    /// <inheritdoc />
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class
    {
        return Task.FromResult(Get<T>(key));
    }

    /// <inheritdoc />
    public void Refresh(string key)
    {
        if (!_cache.TryGetValue(key, out LocalCacheEntry? cacheEntry))
        {
            return;
        }

        cacheEntry.Refresh();
    }

    /// <inheritdoc />
    public Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        Refresh(key);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Remove(string key)
    {
        _ = _cache.TryRemove(key, out _);
    }

    /// <inheritdoc />
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        Remove(key);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public void Set<T>(string key, T value, LocalCacheEntryOptions options)
        where T : class
    {
        _cache[key] = new LocalCacheEntry(value, options);
    }

    /// <inheritdoc />
    public Task SetAsync<T>(
        string key,
        T value,
        LocalCacheEntryOptions options,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        Set(key, value, options);

        return Task.CompletedTask;
    }
}
