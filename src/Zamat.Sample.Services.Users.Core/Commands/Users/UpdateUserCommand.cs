using Zamat.Common.Command;

namespace Zamat.Sample.Services.Users.Core.Commands.Users;

public record UpdateUserCommand(string Id, string FirstName, string LastName) : ICommand
{
}
