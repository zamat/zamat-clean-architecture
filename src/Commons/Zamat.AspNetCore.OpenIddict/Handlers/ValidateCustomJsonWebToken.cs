using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static OpenIddict.Validation.OpenIddictValidationEvents;
using static OpenIddict.Validation.OpenIddictValidationHandlers.Protection;

namespace Zamat.AspNetCore.OpenIddict.Handlers;

class ValidateCustomJsonWebToken : IOpenIddictValidationHandler<ValidateTokenContext>
{
    private readonly TokenValidationParameters _tokenValidationParameters;

    public ValidateCustomJsonWebToken(TokenValidationParameters tokenValidationParameters)
    {
        _tokenValidationParameters = tokenValidationParameters;
    }

    public static OpenIddictValidationHandlerDescriptor Descriptor { get; }
        = OpenIddictValidationHandlerDescriptor.CreateBuilder<ValidateTokenContext>()
            .UseSingletonHandler<ValidateCustomJsonWebToken>()
            .SetOrder(ValidateIdentityModelToken.Descriptor.Order + 500)
            .SetType(OpenIddictValidationHandlerType.Custom)
            .Build();

    public ValueTask HandleAsync(ValidateTokenContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Principal is not null)
        {
            return default;
        }

        var tokenHandler = new JsonWebTokenHandler();

        var result = tokenHandler.ValidateToken(context.Token, _tokenValidationParameters);
        if (result is not { IsValid: true })
        {
            return default;
        }

        context.Principal = new ClaimsPrincipal(result.ClaimsIdentity).SetTokenType(TokenTypeHints.AccessToken);

        return default;
    }
}
