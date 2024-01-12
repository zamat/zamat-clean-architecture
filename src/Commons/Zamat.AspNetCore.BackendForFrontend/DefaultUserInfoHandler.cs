using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AUMS.AspNetCore.BackendForFrontend;

internal class DefaultUserInfoHandler : IUserInfoHandler
{
    private readonly IHostEnvironment _env;

    public DefaultUserInfoHandler(IHostEnvironment env)
    {
        _env = env;
    }

    public async Task HandleAsync(HttpContext context)
    {
        var result = await context.AuthenticateAsync();

        if (!result.Succeeded)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(Serialize(new UserInfoResponse(false, null, new List<string>())), Encoding.UTF8);
            return;
        }

        var userInfo = new UserInfo()
        {
            Id = result.Principal.FindFirstValue("sub"),
            TenantId = result.Principal.FindFirstValue("tenant"),
            Email = result.Principal.FindFirstValue(ClaimTypes.Email),
            UserName = result.Principal.FindFirstValue("name"),
            FirstName = result.Principal.FindFirstValue("given_name"),
            LastName = result.Principal.FindFirstValue("family_name"),
            CorrespondenceEmail = result.Principal.FindFirstValue("correspondence_email"),
            CorrespondencePhoneNumber = result.Principal.FindFirstValue("correspondence_phoneNumber"),
            Realm = result.Principal.FindFirstValue("realm"),
            Issuer = result.Principal.FindFirstValue("iss")
        };

        foreach (var role in result.Principal.FindAll(c => c.Type == ClaimTypes.Role))
        {
            userInfo.AddRole(role.Value);
        }

        var permissions = result.Principal.FindAll(Consts.PermissionClaim);

        var response = new UserInfoResponse(true, userInfo, permissions.Select(c => c.Value))
        {
        };

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(Serialize(response), Encoding.UTF8);
    }

    private static string Serialize(UserInfoResponse userInfo)
    {
        return JsonSerializer.Serialize(userInfo, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
