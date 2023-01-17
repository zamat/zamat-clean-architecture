using Microsoft.Extensions.Logging;
using Zamat.Sample.Cli;
using Zamat.Sample.Services.Users.Infrastructure;

var builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureServices((host, services) =>
{
    services.AddUsersDbContext(host.Configuration);
});

builder.ConfigureLogging(configureLogging =>
{
    configureLogging.ClearProviders().AddConsole();
});

var app = builder.Build();

app.AddCommands<Console>();

app.Run();