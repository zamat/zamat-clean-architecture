using AUMS.AspNetCore.Authorization.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AUMS.AspNetCore.Authentication.ApiKeyStore;

public static class AuthorizationOptionsExtensions
{
    private const string AumsClaimPolicyName = "AumsApiClaimPolicy";

    /// <summary>
    /// Adds a <see cref="ClaimsAuthorizationRequirement"/> to the current instance which requires
    /// that the current user has the <see cref="AumsClaimTypes.AumsAccess"/> claim and that the claim value must be one of the allowed values.
    /// </summary>
    /// <param name="options"><see cref="AuthorizationOptions"/> to extend.</param>
    /// <param name="allowedValues">Values the claim must process one or more of for evaluation to succeed.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static AuthorizationOptions AddAumsAccessPolicy(
        this AuthorizationOptions options,
        params string[] allowedValues
    )
    {
        options.AddPolicy(
            AumsClaimPolicyName,
            policy => policy.RequireClaim(AumsClaimTypes.AumsAccess, allowedValues)
        );

        return options;
    }
}
