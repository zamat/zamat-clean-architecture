using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zamat.Clean.Cli.SeedData;
using Zamat.Clean.Services.Users.Core.Domain.Entities;
using Zamat.Clean.Services.Users.Core.Domain.ValueObjects;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;

namespace Zamat.Clean.Cli;

class Console(UsersDbContext dbContext, ILogger<Console> logger) : ConsoleAppBase
{
    private readonly UsersDbContext _dbContext = dbContext;
    private readonly ILogger<Console> _logger = logger;

    [Command("ef-core-migration", "Migrate EFCore database.")]
    public async Task MigrateAsync()
    {
        _logger.LogInformation($"Database migration started.");

        if (_dbContext.Database.IsRelational())
        {
            await _dbContext.Database.MigrateAsync();
        }

        _logger.LogInformation("Database migration completed.");
    }

    [Command("initialize", "Migrate EFCore database and seed data.")]
    public async Task InitializeAsync()
    {
        _logger.LogInformation($"Run initialize method.");

        await MigrateAsync();
        await LoadSeedDataAsync();
    }

    [Command("seed-data", "Load seed data.")]
    public async Task LoadSeedDataAsync([Option("f", "filename")] string filename = "seeds.json", [Option("run-migration", "Run migration")] bool runMigration = false, CancellationToken cancellationToken = default)
    {
        if (runMigration)
        {
            await MigrateAsync();
        }

        _logger.LogInformation("Loading seed data started.");

        var data = Load<Seed>(filename);

        await LoadUsersAsync(data.Users, cancellationToken);

        _logger.LogInformation("Loading seed data completed.");
    }

    async Task LoadUsersAsync(IEnumerable<SeedUser> users, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Loading users started.");

        foreach (var user in users)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == user.Id, cancellationToken))
            {
                _dbContext.Add(new User(user.Id, user.UserName, new FullName(user.FirstName, user.LastName)));
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Loading users completed.");
    }

    static T Load<T>(string filename)
    {
        var opt = new JsonSerializerOptions();
        opt.Converters.Add(new JsonStringEnumConverter());
        var fileContent = File.ReadAllText(filename);
        return JsonSerializer.Deserialize<T>(fileContent, opt)!;
    }
}
