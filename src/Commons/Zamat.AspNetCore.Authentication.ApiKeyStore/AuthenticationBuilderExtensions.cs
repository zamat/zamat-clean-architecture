using AUMS.AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Authentication;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore;

public static class AuthenticationBuilderExtensions
{
    /// <summary>
    /// Configures the authentication builder to use an AUMS API key store.
    /// </summary>
    /// <param name="builder">The authentication builder.</param>
    /// <param name="action">An action to configure the API key store builder.</param>
    /// <returns>The same authentication builder so that multiple calls can be chained.</returns>
    public static AuthenticationBuilder AddAumsApiKeyStore(
        this AuthenticationBuilder builder,
        Action<AumsApiKeyStoreConfigurator> action
    )
    {
        var apiKeyBuilder = new AumsApiKeyStoreConfigurator(builder.Services);

        action.Invoke(apiKeyBuilder);
        apiKeyBuilder.ValidateConfiguration();

        return builder.AddApiKey(configure =>
        {
            configure.UseStore<AumsApiKeyStore>();
        });
    }
}
