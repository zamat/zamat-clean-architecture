using System;

namespace Zamat.Common.Command.Bus;

public class CommandBusException : Exception
{
    public CommandBusException(string message) : base(message) { }
}
