using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class RedirectToIdpForSignOutEventHandler
{
    public RedirectToIdpForSignOutEventHandler()
    {
    }

    public Task HandleAsync(RedirectContext context)
    {
        var realm = context.Properties.GetParameter<string>(Consts.Realm);

        context.ProtocolMessage.AcrValues = $"realm:{realm}";

        return Task.CompletedTask;
    }
}
