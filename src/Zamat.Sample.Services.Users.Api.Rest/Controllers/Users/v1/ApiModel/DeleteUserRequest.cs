using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1.ApiModel;

public record DeleteUserRequest
{
    [SwaggerSchema("The user identifier.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    public string UserId { get; init; } = default!;
}
