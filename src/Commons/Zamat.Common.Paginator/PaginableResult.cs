using System.Collections.Generic;
using System.Linq;

namespace Zamat.Common.Paginator;

public record PaginableResult<TResult>(int CurrentPage = 1, int ItemsPerPage = 20, int TotalItems = 0, IEnumerable<TResult>? Items = default)
{
    public IEnumerable<TResult> Items { get; init; } = Items ?? Enumerable.Empty<TResult>();
    public int TotalPages => TotalItems > 0 ? (TotalItems % ItemsPerPage > 0 ? 1 : 0) + TotalItems / ItemsPerPage : 0;
    public int FirstRowOnPage => (CurrentPage - 1) * ItemsPerPage + 1;
}