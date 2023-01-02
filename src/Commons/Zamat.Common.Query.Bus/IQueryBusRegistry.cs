namespace Zamat.Common.Query.Bus;

public interface IQueryBusRegistry
{
    THandler GetQueryHandler<THandler>();
}
