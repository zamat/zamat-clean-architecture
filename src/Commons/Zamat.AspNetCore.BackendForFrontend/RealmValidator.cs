using System.Security.Claims;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class RealmValidator : IRealmValidator
{
    public async Task<bool> TryValidateAsync(HttpContext context, string realm, string returnUrl)
    {
        var authenticateResult = await context.AuthenticateAsync();
        if (!authenticateResult.Succeeded)
        {
            return true;
        }

        var currentRealm = authenticateResult.Principal.FindFirstValue(Consts.Realm);
        if (realm != currentRealm)
        {
            var forbidProperties = new AuthenticationProperties
            {
                RedirectUri = returnUrl + "?error_code=" + ErrorCodes.InvalidRealm
            };
            await context.ForbidAsync(CookieAuthenticationDefaults.AuthenticationScheme, forbidProperties);
            return false;
        }

        return true;
    }
}
