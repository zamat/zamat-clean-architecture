using Microsoft.AspNetCore.Authentication;

namespace AUMS.AspNetCore.Authentication.ApiKey;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddApiKey(
        this AuthenticationBuilder builder,
        Action<ApiKeyBuilder> configure
    )
    {
        var apiKeyBuilder = new ApiKeyBuilder(builder.Services);
        configure(apiKeyBuilder);

        return builder;
    }
}
