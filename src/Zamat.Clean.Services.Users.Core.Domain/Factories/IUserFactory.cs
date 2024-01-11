using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Core.Domain.ValueObjects;

namespace Zamat.Clean.Services.Users.Core.Domain.Factories;

public interface IUserFactory
{
    User Create(string id, string userName, FullName fullName);
}
