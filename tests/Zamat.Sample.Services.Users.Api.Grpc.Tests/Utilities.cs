using Zamat.Sample.Services.Users.Core.Domain.Entities;
using Zamat.Sample.Services.Users.Core.Domain.ValueObjects;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;

namespace Zamat.Sample.Services.Users.Api.Grpc.Tests;

public static class Utilities
{
    public static void InitializeDbForTests(UsersDbContext db)
    {
        db.AddRange(GetSeedingUsers());
        db.SaveChanges();
    }
    public static void ClearDbForTests(UsersDbContext db)
    {
        db.RemoveRange(db.Users);
        db.SaveChanges();
    }

    static List<User> GetSeedingUsers()
    {
        return new List<User>() {
            new User("u100", "john.doe@constoso.com", new FullName("John", "Doe")),
            new User("u101", "lucy.doe@constoso.com", new FullName("Lucy", "Doe")),
            new User("u102", "jack.doe@constoso.com", new FullName("Jack", "Doe"))
        };
    }
}
