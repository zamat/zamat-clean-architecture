using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Zamat.AspNetCore.Tests;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;

namespace Zamat.Sample.Services.Users.Api.Grpc.Tests;

public class UsersWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddFakeAuthentication();

            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(UsersDbContext));
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();

            var scopedServices = scope.ServiceProvider;

            var logger = scopedServices.GetRequiredService<ILogger<UsersWebApplicationFactory>>();

            var db = scopedServices.GetRequiredService<UsersDbContext>();

            db.Database.EnsureCreated();

            try
            {
                Utilities.ClearDbForTests(db);
                Utilities.InitializeDbForTests(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
            }
        });
    }
}
