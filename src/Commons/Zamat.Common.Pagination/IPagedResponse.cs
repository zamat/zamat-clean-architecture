using System.Collections.Generic;

namespace AUMS.Common.Pagination;

/// <summary>
/// Paged resource result containing the total number of all
/// available resources and information about the current page.
/// </summary>
/// <typeparam name="T">Type of resource returned.</typeparam>
public interface IPagedResponse<T>
{
    /// <summary>
    /// Currently requested resource page with number of
    /// elements specified in <see cref="ItemsPerPage"/>.
    /// </summary>
    int CurrentPage { get; }

    /// <summary>
    /// Total number of all pages possible to retrieve when
    /// dividing the total number of all resources by <see cref="ItemsPerPage"/>.
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// How many resources should be in each page.
    /// </summary>
    int ItemsPerPage { get; }

    /// <summary>
    /// Total amount of available resources.
    /// </summary>
    long TotalItems { get; }

    /// <summary>
    /// Resources for the <see cref="CurrentPage"/>.
    /// </summary>
    IEnumerable<T> Items { get; }
}
