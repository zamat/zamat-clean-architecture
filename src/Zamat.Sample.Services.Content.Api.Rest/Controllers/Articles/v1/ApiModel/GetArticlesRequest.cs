using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Sample.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record GetArticlesRequest
{
    [DefaultValue(1)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [SwaggerSchema("The page index.", Nullable = false)]
    public int Page { get; init; } = 1;

    [DefaultValue(5)]
    [Required(ErrorMessage = "The {0} field is required.")]
    [SwaggerSchema("The page limit index.", Nullable = false)]
    public int Limit { get; init; } = 5;

    [DefaultValue(SortBy.IdDesc)]
    [SwaggerSchema("The sort by policy", Nullable = false)]
    [Required(ErrorMessage = "The {0} field is required.")]
    public SortBy SortBy { get; init; } = SortBy.IdDesc;
}

public enum SortBy
{
    IdAsc,
    IdDesc
}
