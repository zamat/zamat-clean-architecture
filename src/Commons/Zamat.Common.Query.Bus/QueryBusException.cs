using System;

namespace Zamat.Common.Query.Bus;

public class QueryBusException(string message) : Exception(message)
{
}
