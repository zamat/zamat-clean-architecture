using AUMS.AspNetCore.Authentication.ApiKeyStore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore;

/// <summary>
/// Provides a builder for configuring and registering API key store related services.
/// </summary>
public class AumsApiKeyStoreConfigurator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AumsApiKeyStoreConfigurator"/> class with the specified services.
    /// </summary>
    /// <param name="services">The collection of services to add the API key store to.</param>
    public AumsApiKeyStoreConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    /// <summary>
    /// Gets the collection of services that the API key store is being added to.
    /// </summary>
    public IServiceCollection Services { get; }

    public void ValidateConfiguration()
    {
        ValidateRepositoryRegistration();
    }

    private void ValidateRepositoryRegistration()
    {
        var descriptor = Services.FirstOrDefault(
            x => x.ServiceType == typeof(IAumsApiKeyRepository)
        );

        if (descriptor is null)
        {
            throw new InvalidOperationException(
                $"There is no registered service providing the {nameof(IAumsApiKeyRepository)} repository for {nameof(AumsApiKeyStore)}. You need to register {nameof(IAumsApiKeyRepository)}."
            );
        }
    }
}
