using System.Security.Claims;

namespace AUMS.AspNetCore.Authentication.ApiKey;

public class ApiKey
{
    public ApiKey(string key, string owner)
    {
        Key = key;
        Owner = owner;
    }

    public string Key { get; init; }

    public string Owner { get; init; }

    public List<Claim> Claims { get; private set; } = new();

    public ApiKey AddClaims(IEnumerable<Claim> claims)
    {
        Claims.AddRange(claims);
        return this;
    }

    public ApiKey AddClaim(Claim claim)
    {
        Claims.Add(claim);
        return this;
    }
}
