using System.Threading.Tasks;
using AUMS.AspNetCore.Opa.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.Opa;

internal class OpaRequirementHandler : AuthorizationHandler<OpaRequirement>
{
    private readonly IOpaEvaluator _opaEvaluator;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public OpaRequirementHandler(IOpaEvaluator opaEvaluator, IHttpContextAccessor httpContextAccessor)
    {
        _opaEvaluator = opaEvaluator;
        _httpContextAccessor = httpContextAccessor;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, OpaRequirement requirement)
    {
        var input = new Input
        {
            User = context.User.FindFirst("sub")?.Value!,
            Feature = requirement.OpaFeature
        };

        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext.Request is not null)
        {
            var httpRequest = httpContext.Request;
            input.Request = new Request()
            {
                Protocol = httpRequest.Protocol,
                Host = httpRequest.Host.Value,
                Scheme = httpRequest.Scheme,
                Method = httpRequest.Method,
                QueryString = string.IsNullOrEmpty(httpRequest.QueryString.Value) ? "?=" : httpRequest.QueryString.Value,
                PathBase = string.IsNullOrEmpty(httpRequest.PathBase) ? "/" : httpRequest.PathBase,
                Path = httpRequest.Path,
                TraceId = httpContext.TraceIdentifier
            };

            foreach (var header in httpRequest.Headers)
            {
                input.Request.Headers.Add(header.Key.ToLower(), header.Value!);
            }
        }

        if (httpContext.Connection is not null)
        {
            var connection = httpContext.Connection;
            input.Connection = new Connection()
            {
                Id = connection.Id,
                LocalIpAddress = connection.LocalIpAddress?.ToString(),
                RemoteIpAddress = connection.RemoteIpAddress?.ToString()
            };
        }

        var output = await _opaEvaluator.EvaluateAsync("app/rbac/allow", input);

        if (output?.Result == true)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
