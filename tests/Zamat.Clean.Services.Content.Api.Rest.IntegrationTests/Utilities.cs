using Zamat.Clean.Services.Content.Infrastructure.EFCore;

namespace Zamat.Clean.Services.Content.Api.Rest.IntegrationTests;

public static class Utilities
{
    public static void ClearDbForTests(ContentDbContext db)
    {
        db.RemoveRange(db.Articles);
        db.SaveChanges();
    }
}
