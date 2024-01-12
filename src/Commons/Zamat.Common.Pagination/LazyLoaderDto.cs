using System.Collections.Generic;

namespace AUMS.Common.Pagination;

/// <summary>
/// Used when we want to asynchronously retrieve resources
/// in batches, but don't want to check the total number
/// of resources each time.
/// </summary>
/// <typeparam name="T">Type of resource returned.</typeparam>
public record LazyLoaderDto<T>(long Taken, long Skipped, IEnumerable<T> Items) : ILazyResponse<T>;
