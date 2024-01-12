using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class DefaultLogoutHandler : ILogoutHandler
{
    public DefaultLogoutHandler()
    {
    }

    public async Task HandleAsync(HttpContext context)
    {
        var returnUrl = context.Request.Query[Consts.ReturnUrl].FirstOrDefault() ?? "/";

        var result = await context.AuthenticateAsync();
        if (!result.Succeeded)
        {
            context.Response.Redirect(returnUrl);
            return;
        }

        var properties = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };

        properties.SetParameter(Consts.Realm, result.Principal.FindFirstValue(Consts.Realm) ?? string.Empty);

        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, properties);
        await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
    }
}
