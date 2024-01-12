using System.Threading.Tasks;
using AUMS.AspNetCore.BackendForFrontend.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.BackendForFrontend;

internal static class EndpointRouteBuilderExtensions
{
    public static void AddLoginEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/login", Handle<ILoginHandler>)
            .WithDisplayName("Login")
            .AllowAnonymous();
    }

    public static void AddLogoutEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/logout", Handle<ILogoutHandler>)
            .WithDisplayName("Logout")
            .AllowAnonymous();
    }

    public static void AddUserInfoEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/userinfo", Handle<IUserInfoHandler>)
            .WithDisplayName("GetUserInfo")
            .AllowAnonymous();
    }

    public static void AddAccessDeniedEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/accessDenied", Handle<IAccessDeniedHandler>)
            .WithDisplayName("AccessDenied")
            .AllowAnonymous();
    }

    private static Task Handle<T>(HttpContext context) where T : IEndpointHandler
    {
        return context.RequestServices.GetRequiredService<T>().HandleAsync(context);
    }
}
