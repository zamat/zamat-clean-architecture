using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Core.Domain.Factories;
using Zamat.Clean.Services.Users.Core.Domain.ValueObjects;

namespace Zamat.Clean.Services.Users.Infrastructure.Factories;

internal class UserFactory : IUserFactory
{
    public User Create(string id, string userName, FullName fullName)
    {
        return User.Create(id, userName, fullName);
    }
}
