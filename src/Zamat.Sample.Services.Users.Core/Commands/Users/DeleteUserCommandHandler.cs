using Zamat.Common.Command;
using Zamat.Common.Events.Bus;

namespace Zamat.Sample.Services.Users.Core.Commands.Users;

class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public DeleteUserCommandHandler(IApplicationUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<CommandResult> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetOrDefaultAsync(new UserWithIdSpec(command.Id), cancellationToken);
        if (user is null)
        {
            return new CommandResult(new PreconditionError(CommandErrorCode.InvalidUser, "User with given id not found."));
        }

        await _unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);

        await _eventBus.PublishAsync(new UserDeleted(user), cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CommandResult();
    }
}
