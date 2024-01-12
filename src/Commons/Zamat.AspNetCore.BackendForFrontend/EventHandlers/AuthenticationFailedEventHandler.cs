using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class AuthenticationFailedEventHandler
{
    public AuthenticationFailedEventHandler()
    {
    }

    public Task HandleAsync(AuthenticationFailedContext context)
    {
        _ = context;

        return Task.CompletedTask;
    }
}
