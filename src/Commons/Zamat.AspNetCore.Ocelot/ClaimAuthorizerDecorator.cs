using System.Security.Claims;
using Ocelot.Authorization;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Responses;

namespace Zamat.AspNetCore.Ocelot;

internal class ClaimAuthorizerDecorator : IClaimsAuthorizer
{
    private readonly ClaimsAuthorizer _authorizer;

    public ClaimAuthorizerDecorator(ClaimsAuthorizer authorizer)
    {
        _authorizer = authorizer;
    }

    public Response<bool> Authorize(ClaimsPrincipal claimsPrincipal, Dictionary<string, string> routeClaimsRequirement, List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)
    {
        var newRouteClaimsRequirement = new Dictionary<string, string>();
        foreach (var kvp in routeClaimsRequirement)
        {
            if (kvp.Key.StartsWith("claimtype/"))
            {
                var key = kvp.Key.Replace("claimtype/", "http://");
                newRouteClaimsRequirement.Add(key, kvp.Value);
            }
            else
            {
                newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
            }
        }

        return _authorizer.Authorize(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
    }
}
