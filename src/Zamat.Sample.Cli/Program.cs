using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using Zamat.Sample.Cli;
using Zamat.Sample.Services.Users.Core;
using Zamat.Sample.Services.Users.Infrastructure;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureLogging(builder => builder.ClearProviders().AddConfiguration(configuration.GetSection("Logging")).AddConsole());

builder.ConfigureServices(services =>
{
    services.AddCore()
    .AddInfrastructure(configuration);
});

var app = builder.Build();

app.AddCommands<Console>();

app.Run();