using System.Collections.Generic;

namespace AUMS.Common.Pagination;

/// <summary>
/// Used when we want to asynchronously retrieve resources
/// in batches, but don't want to check the total number
/// of resources each time.
/// </summary>
/// <typeparam name="T">Type of resource returned.</typeparam>
public interface ILazyResponse<T>
{
    /// <summary>
    /// How many resources are to be retrieved in the current request.
    /// </summary>
    long Taken { get; }

    /// <summary>
    /// How many resources are to be skipped.
    /// </summary>
    long Skipped { get; }

    /// <summary>
    /// Resources requested, except amount skipped.
    /// </summary>
    IEnumerable<T> Items { get; }
}
