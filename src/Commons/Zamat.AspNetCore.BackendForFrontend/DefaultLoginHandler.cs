using System;
using System.Linq;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class DefaultLoginHandler : ILoginHandler
{
    private readonly IRealmValidator _realmValidator;

    public DefaultLoginHandler(IRealmValidator realmValidator)
    {
        _realmValidator = realmValidator;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var returnUrl = context.Request.Query[Consts.ReturnUrl].FirstOrDefault() ?? "/";

        var realm = context.Request.Query[Consts.Realm].FirstOrDefault();

        if (string.IsNullOrEmpty(realm))
        {
            throw new Exception("Realm authentication property is required");
        }

        if (!await _realmValidator.TryValidateAsync(context, realm, returnUrl))
        {
            return;
        }

        var properties = new AuthenticationProperties
        {
            RedirectUri = returnUrl
        };

        properties.SetParameter(Consts.Realm, realm);

        await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, properties);
    }
}
