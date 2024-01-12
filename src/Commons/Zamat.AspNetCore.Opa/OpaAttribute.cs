using Microsoft.AspNetCore.Authorization;

namespace AUMS.AspNetCore.Opa;

public class OpaAttribute : AuthorizeAttribute
{
    private string _feature = string.Empty;
    public string OpaFeature
    {
        get
        {
            return _feature;
        }
        set
        {
            _feature = value;
            Policy = Constants.PolicyName + _feature;
        }
    }
}
