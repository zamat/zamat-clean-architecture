namespace AUMS.Common.Pagination;

/// <summary>
/// Interface for REST contracts to harden the consistency of lazy loaded requests.
/// <para>Should be used with common <see cref="LazyLoaderDto{T}"/>.</para>
/// </summary>
public interface ILazyRequest
{
    /// <summary>
    /// Gets or sets the number of elements to take in the request.
    /// </summary>
    long Take { get; }

    /// <summary>
    /// Gets or sets the number of elements to skip in the request.
    /// </summary>
    long Skip { get; }
}
