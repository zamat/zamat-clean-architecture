using Zamat.Common.Command;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

public record DeleteUserCommand(string Id) : ICommand
{
}
