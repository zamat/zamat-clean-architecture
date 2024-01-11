using Swashbuckle.AspNetCore.Annotations;
using Zamat.Clean.Services.Content.Core.Entities;

namespace Zamat.Clean.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record GetArticleResponse()
{
    [SwaggerSchema("The article identifier.")]
    public string Id { get; init; } = default!;

    [SwaggerSchema("The article title.", Nullable = false)]
    public string Title { get; init; } = default!;

    [SwaggerSchema("The article text.")]
    public string Text { get; init; } = default!;

    internal GetArticleResponse(Article article) : this()
    {
        Id = article.Id;
        Title = article.Title;
        Text = article.Text;
    }
}
