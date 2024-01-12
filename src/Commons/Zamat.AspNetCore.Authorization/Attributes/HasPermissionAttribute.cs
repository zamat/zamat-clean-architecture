using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Zamat.AspNetCore.Authorization.Claims;
using Zamat.AspNetCore.Authorization.Filters;

namespace Zamat.AspNetCore.Authorization.Attributes;

/// <summary>
/// Confirms that claims principal has specific permission claim using <see cref="ClaimAuthorizationFilter"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute : TypeFilterAttribute
{
    public HasPermissionAttribute(string permission) : base(typeof(ClaimAuthorizationFilter))
    {
        Arguments = new object[]
        {
            new Claim(AumsClaimTypes.Permission, permission)
        };
    }
}
