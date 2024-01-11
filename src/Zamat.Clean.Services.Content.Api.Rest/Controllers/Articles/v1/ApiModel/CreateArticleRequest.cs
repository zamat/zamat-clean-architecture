using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Clean.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record CreateArticleRequest
{
    [SwaggerSchema("The article title.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [MinLength(6, ErrorMessage = "The field {0} must have a minimum length of {1}.")]
    public string Title { get; init; } = default!;

    [SwaggerSchema("The article text.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    public string Text { get; init; } = default!;
}
