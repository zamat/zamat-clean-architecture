using Zamat.Sample.Services.Content.Infrastructure.EFCore;

namespace Zamat.Sample.Services.Content.Api.Rest.IntegrationTests;

public static class Utilities
{
    public static void ClearDbForTests(ContentDbContext db)
    {
        db.RemoveRange(db.Articles);
        db.SaveChanges();
    }
}
