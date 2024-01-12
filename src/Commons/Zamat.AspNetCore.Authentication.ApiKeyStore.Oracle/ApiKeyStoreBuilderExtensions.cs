using AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle.Repositories;
using AUMS.AspNetCore.Authentication.ApiKeyStore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore.Oracle;

public static class ApiKeyStoreBuilderExtensions
{
    public const string AumsApiKeyStoreConnectionStringName = "AumsApiKeyStoreDbContext";

    /// <summary>
    /// Configures the API key store builder to use an Oracle database.
    /// </summary>
    /// <param name="configurator">The API key store builder.</param>
    /// <param name="configuration">The application configuration, which is expected to contain a connection string for the AUMS API key store.</param>
    public static void UseOracle(
        this AumsApiKeyStoreConfigurator configurator,
        IConfiguration configuration
    )
    {
        configurator.Services.AddPersistence(configuration);

        configurator.Services.AddScoped<IAumsApiKeyRepository, AumsApiKeyRepository>();
    }

    private static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var apiKeyStoreConnectionString =
            configuration.GetConnectionString(AumsApiKeyStoreConnectionStringName)
            ?? throw new InvalidOperationException(
                $"AUMS API Key store requires {AumsApiKeyStoreConnectionStringName} connection string."
            );

        _ = services.AddDbContext<OracleApiKeyDbContext>(
            o => o.UseOracle(apiKeyStoreConnectionString)
        );
    }
}
