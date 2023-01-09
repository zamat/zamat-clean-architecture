using Swashbuckle.AspNetCore.Annotations;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record UserPaginatorResponse(
    [SwaggerSchema("The user paginator current page.")]
    int CurrentPage = 1,
    [SwaggerSchema("The user paginator items per page.")]
    int ItemsPerPage = 20,
    [SwaggerSchema("The user paginator total items.")]
    int TotalItems = 0
    )
{
    [SwaggerSchema("The user paginator total pages.")]
    public int TotalPages => TotalItems > 0 ? (TotalItems % ItemsPerPage > 0 ? 1 : 0) + TotalItems / ItemsPerPage : 0;
    [SwaggerSchema("The user paginator items.")]
    public IEnumerable<UserResponse> Items { get; init; } = new List<UserResponse>();
}
