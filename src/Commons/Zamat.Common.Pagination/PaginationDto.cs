using System.Collections.Generic;

namespace AUMS.Common.Pagination;

/// <summary>
/// Paged resource result containing the total number of all
/// available resources and information about the current page.
/// </summary>
/// <param name="CurrentPage">Currently requested resource page with number of
/// elements specified in <see cref="ItemsPerPage"/>.</param>
/// <param name="TotalPages">Total number of all pages possible to retrieve when
/// dividing the total number of all resources by <see cref="ItemsPerPage"/>.</param>
/// <param name="ItemsPerPage">How many resources should be in each page.</param>
/// <param name="TotalItems">Total amount of available resources.</param>
/// <param name="Items">Resources for the <see cref="CurrentPage"/>.</param>
/// <typeparam name="T">Type of resource returned.</typeparam>
public record PaginationDto<T>(
    int CurrentPage,
    int TotalPages,
    int ItemsPerPage,
    long TotalItems,
    IEnumerable<T> Items
) : IPagedResponse<T>;
