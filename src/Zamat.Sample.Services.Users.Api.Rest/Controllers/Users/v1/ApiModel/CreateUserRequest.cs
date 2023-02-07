using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1.ApiModel;

public record CreateUserRequest
{
    [SwaggerSchema("The user name.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [MinLength(6, ErrorMessage = "The field {0} must have a minimum length of {1}.")]
    public string UserName { get; init; } = default!;

    [SwaggerSchema("The user first name.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [MaxLength(50)]
    public string FirstName { get; init; } = default!;

    [SwaggerSchema("The user last name.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [MaxLength(50)]
    public string LastName { get; init; } = default!;
}
