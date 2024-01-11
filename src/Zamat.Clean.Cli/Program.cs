using Microsoft.Extensions.Logging;
using Zamat.Clean.Cli;
using Zamat.Clean.Services.Content.Infrastructure;
using Zamat.Clean.Services.Users.Infrastructure;

var builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureServices((host, services) =>
{
    services.AddUsersDbContext(host.Configuration);
    services.AddContentDbContext(host.Configuration);
});

builder.ConfigureLogging(configureLogging =>
{
    configureLogging.ClearProviders().AddConsole();
});

var app = builder.Build();

app.AddCommands<Console>();

app.Run();