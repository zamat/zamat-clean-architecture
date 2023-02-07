using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

public class FakeAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public List<Claim> Claims { get; set; } = new List<Claim>();
}
