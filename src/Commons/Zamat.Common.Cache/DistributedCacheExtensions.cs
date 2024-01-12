using System;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace AUMS.Common.Cache;

/// <summary>
/// <see cref="IDistributedCache"/> extension methods
/// </summary>
public static class DistributedCacheExtensions
{
    /// <summary>
    /// Retrieves the specified data from the cache, or computes and stores it if it's not already cached.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="key">The key of the data to retrieve.</param>
    /// <param name="action">The function to compute the data if it's not already cached.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The cached data, or the computed data if it was not already cached.</returns>
    public static Task<T> RememberAsync<T>(
        this IDistributedCache distributedCache,
        string key,
        Func<Task<T>> action,
        CancellationToken cancellationToken
    )
    {
        return distributedCache.RememberAsync(
            key,
            action,
            new DistributedCacheEntryOptions(),
            cancellationToken
        );
    }

    /// <summary>
    /// Retrieves the specified data from the cache, or computes and stores it if it's not already cached.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="key">The key of the data to retrieve.</param>
    /// <param name="action">The function to compute the data if it's not already cached.</param>
    /// <param name="options">The options for storing the data.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The cached data, or the computed data if it was not already cached.</returns>
    public static async Task<T> RememberAsync<T>(
        this IDistributedCache distributedCache,
        string key,
        Func<Task<T>> action,
        DistributedCacheEntryOptions options,
        CancellationToken cancellationToken
    )
    {
        var cachedData = await distributedCache.GetAsync<T>(key, cancellationToken);

        if (cachedData is not null)
        {
            return cachedData;
        }

        var computedData = await action.Invoke();

        if (computedData is null)
        {
            return computedData;
        }

        await distributedCache.SetAsync<T>(key, computedData, options, cancellationToken);

        return computedData;
    }

    /// <summary>
    /// Stores the specified data in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="key">The key of the data to store.</param>
    /// <param name="value">The data to store.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task SetAsync<T>(
        this IDistributedCache distributedCache,
        string key,
        T value,
        CancellationToken cancellationToken
    )
    {
        return distributedCache.SetAsync<T>(
            key,
            value,
            new DistributedCacheEntryOptions(),
            cancellationToken
        );
    }

    /// <summary>
    /// Stores the specified data in the cache with the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the value to store.</typeparam>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="key">The key of the data to store.</param>
    /// <param name="value">The data to store.</param>
    /// <param name="options">The options for storing the data.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static Task SetAsync<T>(
        this IDistributedCache distributedCache,
        string key,
        T value,
        DistributedCacheEntryOptions options,
        CancellationToken cancellationToken
    )
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var json = JsonSerializer.Serialize(
            value,
            new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreReadOnlyFields = false,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false
            }
        );

        if (json is null)
        {
            throw new NoNullAllowedException("Serialized value cannot be null.");
        }

        var dataToCache = Encoding.UTF8.GetBytes(json);

        return distributedCache.SetAsync(key, dataToCache, options, cancellationToken);
    }

    /// <summary>
    /// Retrieves the specified data from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="distributedCache">The distributed cache.</param>
    /// <param name="key">The key of the data to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The cached data, or null if it's not in the cache.</returns>
    public static async Task<T?> GetAsync<T>(
        this IDistributedCache distributedCache,
        string key,
        CancellationToken cancellationToken
    )
    {
        var cachedData = await distributedCache.GetAsync(key, cancellationToken);

        if (cachedData is null)
        {
            return default(T?);
        }

        var json = Encoding.UTF8.GetString(cachedData);

        return JsonSerializer.Deserialize<T>(
            json,
            new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreReadOnlyFields = false,
                PropertyNameCaseInsensitive = true
            }
        );
    }
}
