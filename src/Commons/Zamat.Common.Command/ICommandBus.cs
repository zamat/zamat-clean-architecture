using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Command;

public interface ICommandBus
{
    Task<CommandResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
}
