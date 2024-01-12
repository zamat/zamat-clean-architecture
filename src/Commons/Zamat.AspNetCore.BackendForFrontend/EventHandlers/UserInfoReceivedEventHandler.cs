using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AUMS.AspNetCore.BackendForFrontend.EventHandlers;

internal class UserInfoReceivedEventHandler
{
    public UserInfoReceivedEventHandler()
    {
    }

    public Task HandleAsync(UserInformationReceivedContext context)
    {
        _ = context;

        return Task.CompletedTask;
    }
}
