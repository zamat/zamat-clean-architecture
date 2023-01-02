using Zamat.Sample.Services.Users.Core.Domain.Entities;
using Zamat.Sample.Services.Users.Core.Domain.Factories;
using Zamat.Sample.Services.Users.Core.Domain.ValueObjects;

namespace Zamat.Sample.Services.Users.Infrastructure.Factories;

class UserFactory : IUserFactory
{
    public User Create(string id, string userName, FullName fullName)
    {
        return User.Create(id, userName, fullName);
    }
}
