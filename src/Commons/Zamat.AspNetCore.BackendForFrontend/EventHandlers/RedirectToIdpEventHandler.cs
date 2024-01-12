using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class RedirectToIdpEventHandler
{
    public RedirectToIdpEventHandler()
    {
    }

    public Task HandleAsync(RedirectContext context)
    {
        var realm = context.Properties.GetParameter<string>(Consts.Realm);

        context.ProtocolMessage.Prompt = "login";
        context.ProtocolMessage.AcrValues = $"realm:{realm}";

        return Task.CompletedTask;
    }
}
