namespace Zamat.Common.Command.Bus;

public interface ICommandBusRegistry
{
    ICommandHandler<TCommand> GetCommandHandler<TCommand>(TCommand command) where TCommand : class, ICommand;
}
