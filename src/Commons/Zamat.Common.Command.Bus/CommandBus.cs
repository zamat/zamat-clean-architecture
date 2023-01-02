using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Command.Bus;

class CommandBus : ICommandBus
{
    private readonly ICommandBusRegistry _commandBusRegistry;

    public CommandBus(ICommandBusRegistry commandBusRegistry)
    {
        _commandBusRegistry = commandBusRegistry;
    }

    public async Task<CommandResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand
    {
        var commandHandler = _commandBusRegistry.GetCommandHandler(command);
        return await commandHandler.HandleAsync(command, cancellationToken);
    }
}