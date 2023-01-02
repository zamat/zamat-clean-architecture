using Zamat.Common.Command;
using Zamat.Common.Events.Bus;
using Zamat.Sample.Services.Users.Core.Domain.Factories;

namespace Zamat.Sample.Services.Users.Core.Commands.Users;

class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    private readonly IUserFactory _userFactory;

    public CreateUserCommandHandler(IApplicationUnitOfWork unitOfWork, IEventBus eventBus, IUserFactory userFactory)
    {
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
        _userFactory = userFactory;
    }

    public async Task<CommandResult> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = _userFactory.Create(command.Id, command.UserName, new FullName(command.FirstName, command.LastName));

        if (await _unitOfWork.UserRepository.CheckExistsAsync(new UserWithUserNameSpec(command.UserName), cancellationToken))
        {
            return new CommandResult(new CommandError(CommandErrorCode.UserNameNotUnique, "User with given userName already exists."));
        }

        #region Using OutBox Pattern with EFCore store

        await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

        await _eventBus.PublishAsync(new UserCreated(user), cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        #endregion

        return new CommandResult();
    }
}
