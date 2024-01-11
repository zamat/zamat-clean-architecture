namespace Zamat.Common.Command.Bus;

class CommandBus(ICommandBusRegistry commandBusRegistry) : ICommandBus
{
    private readonly ICommandBusRegistry _commandBusRegistry = commandBusRegistry;

    public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var commandHandler = _commandBusRegistry.GetCommandHandler(command);
        return await commandHandler.HandleAsync(command, cancellationToken);
    }
}