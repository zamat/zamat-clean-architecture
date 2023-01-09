using System.Collections.Generic;

namespace Zamat.Common.Paginator;

public record PaginableResult<TResult>(int CurrentPage = 1, int ItemsPerPage = 20, int TotalItems = 0)
{
    public int TotalPages => TotalItems > 0 ? (TotalItems % ItemsPerPage > 0 ? 1 : 0) + TotalItems / ItemsPerPage : 0;
    public IEnumerable<TResult> Items { get; init; } = new List<TResult>();
}