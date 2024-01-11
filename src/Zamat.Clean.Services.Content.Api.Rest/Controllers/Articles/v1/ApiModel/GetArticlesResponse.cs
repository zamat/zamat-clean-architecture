using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Clean.Services.Content.Api.Rest.Controllers.Articles.v1.ApiModel;

public record GetArticlesResponse
{
    [SwaggerSchema("The user paginator current page.")]
    public int CurrentPage { get; }

    [SwaggerSchema("The user paginator items per page.")]
    public int ItemsPerPage { get; }

    [SwaggerSchema("The user paginator total items.")]
    public int TotalItems { get; }

    [SwaggerSchema("The user paginator total pages.")]
    public int TotalPages => TotalItems > 0 ? (TotalItems % ItemsPerPage > 0 ? 1 : 0) + TotalItems / ItemsPerPage : 0;

    [SwaggerSchema("The articles paginator items.")]
    public IEnumerable<GetArticleResponse> Items { get; }

    public GetArticlesResponse(int currentPage, int itemsPerPage, int totalItems, IEnumerable<GetArticleResponse> items)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        Items = items;
    }

    public GetArticlesResponse(int currentPage, int itemsPerPage) : this(currentPage, itemsPerPage, 0, new List<GetArticleResponse>()) { }
}
