using System.Security.Claims;
using Zamat.AspNetCore.Authorization.Claims;

namespace Zamat.AspNetCore.Authorization;

/// <summary>
/// Provides extensions for claim based identity.
/// </summary>
public static class ClaimsIdentityExtensions
{
    public static ClaimsIdentity AddPermission(this ClaimsIdentity claimsIdentity, string permission)
    {
        claimsIdentity.AddClaim(new Claim(AumsClaimTypes.Permission, permission));

        return claimsIdentity;
    }
}
