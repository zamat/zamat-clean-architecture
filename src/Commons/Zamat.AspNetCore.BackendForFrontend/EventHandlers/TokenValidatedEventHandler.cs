using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class TokenValidatedEventHandler
{
    public TokenValidatedEventHandler()
    {
    }

    public Task HandleAsync(TokenValidatedContext context)
    {
        _ = context;

        return Task.CompletedTask;
    }
}
