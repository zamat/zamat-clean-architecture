using Zamat.Common.Command;

namespace Zamat.Clean.Services.Users.Core.Commands.Users;

public record UpdateUserCommand(string Id, string FirstName, string LastName) : ICommand
{
}
