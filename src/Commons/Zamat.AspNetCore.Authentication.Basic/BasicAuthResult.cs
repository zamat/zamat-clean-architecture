using System.Security.Claims;

namespace AUMS.AspNetCore.Authentication.Basic;

public record BasicAuthResult(bool IsAuthenticated, IEnumerable<Claim> Claims)
{
}
