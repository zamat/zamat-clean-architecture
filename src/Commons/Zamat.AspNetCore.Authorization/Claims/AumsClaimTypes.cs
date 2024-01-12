namespace Zamat.AspNetCore.Authorization.Claims;

/// <summary>
/// Defines the claim types that are supported by the framework.
/// </summary>
public static class AumsClaimTypes
{
    public const string Permission = ClaimType2023Namespace + "/permission";

    public const string Tenant = ClaimType2023Namespace + "/tenant";

    public const string AumsAccess = ClaimType2023Namespace + "/aumsaccess";

    internal const string ClaimType2023Namespace = "http://schemas.asseco.corp/2023/04/identity/claims";
}
