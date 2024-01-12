using Microsoft.AspNetCore.Authorization;

namespace AUMS.AspNetCore.Opa;

internal class OpaRequirement : IAuthorizationRequirement
{
    public OpaRequirement(string feature)
    {
        OpaFeature = feature;
    }

    public string OpaFeature { get; }
}
