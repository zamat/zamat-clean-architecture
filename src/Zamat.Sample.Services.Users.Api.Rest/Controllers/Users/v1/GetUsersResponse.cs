using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record GetUsersResponse
{
    [SwaggerSchema("The user paginator current page.")]
    public int CurrentPage { get; }

    [SwaggerSchema("The user paginator items per page.")]
    public int ItemsPerPage { get; }

    [SwaggerSchema("The user paginator total items.")]
    public int TotalItems { get; }

    [SwaggerSchema("The user paginator total pages.")]
    public int TotalPages => TotalItems > 0 ? (TotalItems % ItemsPerPage > 0 ? 1 : 0) + TotalItems / ItemsPerPage : 0;
    
    [SwaggerSchema("The user paginator items.")]
    public IEnumerable<GetUserResponse> Items { get; }

    public GetUsersResponse(int currentPage, int itemsPerPage, int totalItems, IEnumerable<GetUserResponse> items)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        Items = items;
    }

    public GetUsersResponse(int currentPage, int itemsPerPage) : this(currentPage, itemsPerPage, 0, new List<GetUserResponse>()) { }
}
