﻿using Zamat.Clean.Services.Users.Core.Domain.Factories;
using Zamat.Clean.Services.Users.Core.IntegrationEvents;
using Zamat.Common.Command;
using Zamat.Common.Events.Bus;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

internal class CreateUserCommandHandler(IApplicationUnitOfWork unitOfWork, IEventBus eventBus, IUserFactory userFactory) : ICommandHandler<CreateUserCommand>
{
    private readonly IApplicationUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEventBus _eventBus = eventBus;
    private readonly IUserFactory _userFactory = userFactory;

    public async Task<CommandResult> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.UserRepository.GetByUserNameAsync(command.UserName, cancellationToken) is not null)
        {
            return new CommandResult(new PreconditionError(CommandErrorCode.UserNameNotUnique, "User with given userName already exists."));
        }

        var user = _userFactory.Create(command.Id, command.UserName, new FullName(command.FirstName, command.LastName));

        await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

        await _eventBus.PublishAsync(new UserCreated(user.Id, user.UserName), cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CommandResult() { EventData = new { UserId = user.Id } };
    }
}