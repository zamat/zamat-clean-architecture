using System;
using System.Threading;
using System.Threading.Tasks;

namespace AUMS.Common.Cache;

/// <summary>
/// <see cref="ILocalCache"/> extension methods
/// </summary>
public static class LocalCacheExtensions
{
    /// <summary>
    /// Retrieves the specified data from the cache, or computes and stores it if it's not already cached.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="localCache">The local cache.</param>
    /// <param name="key">The key of the data to retrieve.</param>
    /// <param name="action">The function to compute the data if it's not already cached.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The cached data, or the computed data if it was not already cached.</returns>
    public static Task<T> RememberAsync<T>(
        this ILocalCache localCache,
        string key,
        Func<Task<T>> action,
        CancellationToken cancellationToken
    )
        where T : class
    {
        return localCache.RememberAsync(
            key,
            action,
            new LocalCacheEntryOptions(),
            cancellationToken
        );
    }

    /// <summary>
    /// Retrieves the specified data from the cache, or computes and stores it if it's not already cached.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="localCache">The local cache.</param>
    /// <param name="key">The key of the data to retrieve.</param>
    /// <param name="action">The function to compute the data if it's not already cached.</param>
    /// <param name="options">The options for storing the data.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The cached data, or the computed data if it was not already cached.</returns>
    public static async Task<T> RememberAsync<T>(
        this ILocalCache localCache,
        string key,
        Func<Task<T>> action,
        LocalCacheEntryOptions options,
        CancellationToken cancellationToken
    )
        where T : class
    {
        var cachedData = await localCache.GetAsync<T>(key, cancellationToken);

        if (cachedData is not null)
        {
            return cachedData;
        }

        T? computedData = await action.Invoke();

        if (computedData is null)
        {
            return computedData;
        }

        await localCache.SetAsync<T>(key, computedData, options, cancellationToken);

        return computedData;
    }

    /// <summary>
    /// Sets the value with the given key.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="localCache">The local cache.</param>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="value">The value to set in the cache.</param>
    public static void Set<T>(this ILocalCache localCache, string key, T value)
        where T : class
    {
        localCache.Set(key, value, new LocalCacheEntryOptions());
    }

    /// <summary>
    /// Sets the value with the given key.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="localCache">The local cache.</param>
    /// <param name="key">A string identifying the requested value.</param>
    /// <param name="value">The value to set in the cache.</param>
    /// <param name="cancellationToken">Optional. The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public static Task SetAsync<T>(
        this ILocalCache localCache,
        string key,
        T value,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        return localCache.SetAsync(key, value, new LocalCacheEntryOptions(), cancellationToken);
    }
}
