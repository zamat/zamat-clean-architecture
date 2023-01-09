using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record DeleteUserRequest
{
    [SwaggerSchema("The user identifier.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    public string UserId { get; init; } = default!;
}
