namespace AUMS.Common.Pagination;

/// <summary>
/// Interface for REST contracts to harden the consistency of paged requests.
/// <para>Should be used with common <see cref="PaginationDto{T}"/>.</para>
/// </summary>
public interface IPagedRequest
{
    /// <summary>
    /// Gets the currently requested resource page with number of
    /// elements specified in <see cref="Limit"/>.
    /// </summary>
    int Page { get; }

    /// <summary>
    /// Gets the value indicating how many resources should be in each page.
    /// </summary>
    int Limit { get; }
}
