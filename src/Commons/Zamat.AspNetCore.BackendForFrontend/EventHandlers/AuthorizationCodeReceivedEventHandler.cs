using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class AuthorizationCodeReceivedEventHandler
{
    public AuthorizationCodeReceivedEventHandler()
    {
    }

    public Task HandleAsync(AuthorizationCodeReceivedContext context)
    {
        return Task.CompletedTask;
    }
}
