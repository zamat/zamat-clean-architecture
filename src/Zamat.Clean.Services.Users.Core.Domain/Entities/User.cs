using Zamat.BuildingBlocks.Domain;

namespace Zamat.Clean.Services.Users.Core.Domain.Entities;

public class User : Entity<string>, IAggregateRoot
{
    public string UserName { get; private set; } = default!;
    public FullName FullName { get; private set; } = default!;

    private User() : base() { }

    public User(string id, string userName, FullName fullName)
    {
        Id = id;
        UserName = userName;
        FullName = fullName;
    }

    public void ChangeFullName(string firstName, string lastName)
    {
        FullName = new FullName(firstName, lastName);
    }

    public static User Create(string id, string userName, FullName fullName)
    {
        var user = new User(id, userName, fullName);
        user.AddDomainEvent(new UserCreatedEvent(id, userName));
        return user;
    }
}
