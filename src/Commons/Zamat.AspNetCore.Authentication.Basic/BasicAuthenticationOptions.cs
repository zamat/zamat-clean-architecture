using Microsoft.AspNetCore.Authentication;

namespace AUMS.AspNetCore.Authentication.Basic;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
    public string Realm { get; set; } = default!;
}
