using AUMS.Clean.Services.Content.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace AUMS.Clean.Services.Content.Api.Rest.IntegrationTests;

public class ContentWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ContentDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ContentDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(ContentDbContext));
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            var scopedServices = scope.ServiceProvider;

            var logger = scopedServices.GetRequiredService<ILogger<ContentWebApplicationFactory>>();

            var db = scopedServices.GetRequiredService<ContentDbContext>();

            db.Database.EnsureCreated();

            try
            {
                Utilities.ClearDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
            }
        });
    }
}
