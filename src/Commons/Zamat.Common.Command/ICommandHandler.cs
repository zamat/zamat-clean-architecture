using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.Command;

public interface ICommandHandler<TCommand> where TCommand : class, ICommand
{
    Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default!);
}
