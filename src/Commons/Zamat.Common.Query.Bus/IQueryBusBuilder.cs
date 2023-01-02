using System.Reflection;

namespace Zamat.Common.Query.Bus;

public interface IQueryBusBuilder
{
    IQueryBusBuilder AddQueryHandlers(Assembly assembly);
}
