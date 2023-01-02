using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace Zamat.AspNetCore.Tests.FakeAuthentication;

public class FakeAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public List<Claim> Claims { get; set; } = new List<Claim>();
}
