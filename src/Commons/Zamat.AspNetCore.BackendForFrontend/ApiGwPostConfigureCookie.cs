using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class ApiGwPostConfigureCookie : IPostConfigureOptions<CookieAuthenticationOptions>
{
    private readonly IDistributedCache _cache;

    public ApiGwPostConfigureCookie(IDistributedCache cache)
    {
        _cache = cache;
    }

    public void PostConfigure(string name, CookieAuthenticationOptions options)
    {
        options.SessionStore = new DistributedCacheTicketStore(_cache);
    }
}
