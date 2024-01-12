using System.Linq;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class DefaultAccessDeniedHandler : IAccessDeniedHandler
{
    public DefaultAccessDeniedHandler()
    {
    }

    public Task HandleAsync(HttpContext context)
    {
        var returnUrl = context.Request.Query[Consts.ReturnUrl].FirstOrDefault() ?? "/";
        context.Response.Redirect(returnUrl);
        return Task.CompletedTask;
    }
}
