using System;

namespace Zamat.Common.Query.Bus;

public class QueryBusException : Exception
{
    public QueryBusException(string message) : base(message) { }
}
