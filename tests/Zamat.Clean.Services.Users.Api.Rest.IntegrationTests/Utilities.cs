using Zamat.Clean.Services.Users.Core.Domain.Aggregates;
using Zamat.Clean.Services.Users.Core.Domain.ValueObjects;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;
using Zamat.Clean.Services.Users.Infrastructure.EFCore.Entities;

namespace Zamat.Clean.Services.Users.Api.Rest.IntegrationTests;

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

    private static List<UserEntity> GetSeedingUsers()
    {
        return [
            new() { Id = "u100", UserName = "john.doe@constoso.com", FirstName = "John", LastName = "Doe" },
            new() { Id = "u101", UserName = "lucy.doe@constoso.com", FirstName = "Lucy", LastName = "Doe" },
            new() { Id = "u102", UserName = "ben.doe@constoso.com", FirstName = "Ben", LastName = "Doe" },
        ];
    }
}
