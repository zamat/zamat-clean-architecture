using System;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend;

public class BackendForFrontendOptions
{
    public string Authority { get; set; } = default!;

    public string? Audience { get; set; } = default!;

    public string ClientId { get; set; } = default!;

    public string ClientSecret { get; set; } = default!;

    public string Scopes { get; set; } = "openid profile email phone offline_access";

    public string CookieName { get; set; } = "_Bff__AuthCookie_";

    public string? CookieDomain { get; set; }

    public SameSiteMode SameSiteMode { get; set; } = SameSiteMode.None;

    public TimeSpan RemoteAuthenticationTimeout { get; set; } = TimeSpan.FromMinutes(120);
}
