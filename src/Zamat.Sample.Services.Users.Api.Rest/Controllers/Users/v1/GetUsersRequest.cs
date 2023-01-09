using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record GetUsersRequest
{
    [DefaultValue(1)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [SwaggerSchema("The page index.", Nullable = false)]
    public int Page { get; init; } = 1;

    [DefaultValue(5)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [SwaggerSchema("The page limit index.", Nullable = false)]
    public int Limit { get; init; } = 5;
}
