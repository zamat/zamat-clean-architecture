using System;

namespace Zamat.Common.Paginator;

public class PaginatorFactory
{
    public IPaginator<TSource> Create<TSource>(IPaginableSource<TSource> source)
    {
        _ = source ?? throw new ArgumentNullException(nameof(source));
        return new Paginator<TSource>(source);
    }
}
