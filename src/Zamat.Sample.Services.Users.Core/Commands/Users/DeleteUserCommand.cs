using Zamat.Common.Command;

namespace Zamat.Sample.Services.Users.Core.Commands.Users;

public record DeleteUserCommand(string Id) : ICommand
{
}
