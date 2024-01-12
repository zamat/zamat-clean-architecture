using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zamat.AspNetCore.Authorization.Filters;

/// <summary>
/// Checks whether the current identity has a claim with an value.
/// </summary>
public class ClaimAuthorizationFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimAuthorizationFilter(Claim claim)
    {
        _claim = claim;
    }

    /// <inheritdoc />
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Claims.Any(
            c => c.Type == _claim.Type && c.Value == _claim.Value))
        {
            context.Result = new ForbidResult();
        }
    }
}
