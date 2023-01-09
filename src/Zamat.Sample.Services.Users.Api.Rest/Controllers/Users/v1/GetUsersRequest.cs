using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record GetUsersRequest
{
    [DefaultValue(1)]
    [SwaggerSchema("The page index.")]
    public int Page { get; init; } = 1;
    [DefaultValue(20)]
    [SwaggerSchema("The page limit index.")]
    public int Limit { get; init; } = 20;
}
