using Zamat.Common.Command;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

public record CreateUserCommand(string Id, string UserName, string FirstName, string LastName) : ICommand
{
}
