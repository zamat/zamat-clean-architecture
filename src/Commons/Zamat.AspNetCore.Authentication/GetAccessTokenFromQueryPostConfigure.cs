using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Zamat.AspNetCore.Authentication;

internal class GetAccessTokenFromQueryPostConfigure : IPostConfigureOptions<JwtBearerOptions>
{
    public GetAccessTokenFromQueryPostConfigure()
    {
    }

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken))
                {
                    var path = context.HttpContext.Request.Path;
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    }
}
