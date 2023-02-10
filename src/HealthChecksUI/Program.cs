var builder = WebApplication.CreateBuilder(args);

builder.Services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

var app = builder.Build();

app.UseRouting()
   .UseEndpoints(config => config.MapHealthChecksUI());

app.Run();

public partial class Program { }
