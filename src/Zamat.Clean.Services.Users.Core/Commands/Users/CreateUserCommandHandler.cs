using Zamat.Clean.Services.Users.Core.Commands;
using Zamat.Clean.Services.Users.Core.UnitOfWork;
using Zamat.Common.Command;
using Zamat.Common.Events.Bus;
using Zamat.Clean.Services.Users.Core.Domain.Factories;
using Zamat.Clean.Services.Users.Core.IntegrationEvents;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

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
        if (await _unitOfWork.UserRepository.GetOrDefaultAsync(new UserWithUserNameSpec(command.UserName), cancellationToken) is not null)
        {
            return new CommandResult(new PreconditionError(CommandErrorCode.UserNameNotUnique, "User with given userName already exists."));
        }

        var user = _userFactory.Create(command.Id, command.UserName, new FullName(command.FirstName, command.LastName));

        #region Using OutBox Pattern with EFCore store

        await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

        await _eventBus.PublishAsync(new UserCreated(user.Id, user.UserName), cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        #endregion

        return new CommandResult();
    }
}
