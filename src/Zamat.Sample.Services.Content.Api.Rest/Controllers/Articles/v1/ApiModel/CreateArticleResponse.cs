using Swashbuckle.AspNetCore.Annotations;
using Zamat.Sample.Services.Content.Core.Entities;

namespace Zamat.Sample.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record CreateArticleResponse()
{
    [SwaggerSchema("The article identifier.")]
    public string Id { get; init; } = default!;

    [SwaggerSchema("The article title.", Nullable = false)]
    public string Title { get; init; } = default!;

    [SwaggerSchema("The article text.")]
    public string Text { get; init; } = default!;

    internal CreateArticleResponse(Article article) : this()
    {
        Id = article.Id;
        Title = article.Title;
        Text = article.Text;
    }
}
