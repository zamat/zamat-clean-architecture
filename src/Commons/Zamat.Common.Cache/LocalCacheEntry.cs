using System;

namespace AUMS.Common.Cache;

internal class LocalCacheEntry
{
    private readonly DateTimeOffset _createdAt = DateTimeOffset.UtcNow;

    private DateTimeOffset _lastRefresh = DateTimeOffset.UtcNow;

    public LocalCacheEntry(object value, LocalCacheEntryOptions options)
    {
        Value = value;
        Options = options;
    }

    public object Value { get; }

    public LocalCacheEntryOptions Options { get; }

    public bool IsExpired()
    {
        if (IsAbsoluteExpirationRelativeToNowExpired())
        {
            return true;
        }

        if (IsAbsoluteExpirationExpired())
        {
            return true;
        }

        if (IsSlidingExpirationExpired())
        {
            return true;
        }

        return false;
    }

    public void Refresh()
    {
        if (Options.SlidingExpiration is null)
        {
            return;
        }

        _lastRefresh = DateTimeOffset.UtcNow;
    }

    private bool IsAbsoluteExpirationRelativeToNowExpired()
    {
        if (Options.AbsoluteExpirationRelativeToNow is null)
        {
            return false;
        }

        return _createdAt + Options.AbsoluteExpirationRelativeToNow < DateTimeOffset.UtcNow;
    }

    private bool IsAbsoluteExpirationExpired()
    {
        if (Options.AbsoluteExpiration is null)
        {
            return false;
        }

        return Options.AbsoluteExpiration.Value.ToUniversalTime() < DateTimeOffset.UtcNow;
    }

    private bool IsSlidingExpirationExpired()
    {
        if (Options.SlidingExpiration is null)
        {
            return false;
        }

        return _lastRefresh + Options.SlidingExpiration < DateTimeOffset.UtcNow;
    }
}
