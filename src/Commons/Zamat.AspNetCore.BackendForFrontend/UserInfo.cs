using System.Collections.Generic;
using System.Security.Claims;

namespace AUMS.AspNetCore.BackendForFrontend;

internal record UserInfo()
{
    public string? Id { get; init; }
    public string? TenantId { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? CorrespondenceEmail { get; init; }
    public string? CorrespondencePhoneNumber { get; init; }
    public List<string> Roles { get; } = new List<string>();
    public string? Realm { get; init; }
    public string? Issuer { get; init; }

    public void AddRole(string role) => Roles.Add(role);

    public Dictionary<string, object> AdditionalClaims { get; } = new Dictionary<string, object>();
    public void SetClaim(Claim claim) => AdditionalClaims[claim.Type] = claim.Value;
}
