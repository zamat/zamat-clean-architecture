using System;

namespace Zamat.AspNetCore.OpenIddict;

public class OidcAuthOptions
{
    public Uri Issuer { get; set; } = default!;
    public string[]? Audiences { get; set; } = default!;
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public bool UseIntrospection { get; set; } = true;
}
