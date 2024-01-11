using Zamat.Clean.Cli;
using Zamat.Clean.Services.Users.Infrastructure;

var builder = ConsoleAppFramework.ConsoleApp.CreateBuilder(args);

builder.ConfigureServices((host, services) =>
{
    services.AddUsersDbContext(host.Configuration);
});

builder.ConfigureLogging(configureLogging =>
{
    configureLogging.ClearProviders().AddConsole();
});

var app = builder.Build();

app.AddCommands<ConsoleCommands>();

app.Run();