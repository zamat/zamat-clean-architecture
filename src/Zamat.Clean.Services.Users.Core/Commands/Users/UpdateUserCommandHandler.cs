using Zamat.Common.Command;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

internal class UpdateUserCommandHandler(IApplicationUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand>
{
    private readonly IApplicationUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CommandResult> HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetOrDefaultAsync(new UserWithIdSpec(command.Id), cancellationToken);
        if (user is null)
        {
            return new CommandResult(new PreconditionError(CommandErrorCode.InvalidUser, "User with given id not found."));
        }

        user.ChangeFullName(command.FirstName, command.LastName);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CommandResult();
    }
}
