using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Clean.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record DeleteArticleRequest
{
    [SwaggerSchema("The article identifier.", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    public string ArticleId { get; init; } = default!;
}
