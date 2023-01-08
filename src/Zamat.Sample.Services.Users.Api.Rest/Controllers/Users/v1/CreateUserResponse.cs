using Swashbuckle.AspNetCore.Annotations;
using Zamat.Sample.Services.Users.Core.Dtos.Users;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

public record CreateUserResponse()
{
    [SwaggerSchema("The user identifier.")]
    public string Id { get; init; } = default!;

    [SwaggerSchema("The user name.")]
    public string UserName { get; init; } = default!;

    [SwaggerSchema("The user first name.", Nullable = false)]
    public string FirstName { get; init; } = default!;

    [SwaggerSchema("The user last name.", Nullable = false)]
    public string LastName { get; init; } = default!;

    internal CreateUserResponse(UserDto userDto) : this()
    {
        Id = userDto.Id;
        UserName = userDto.UserName;
        FirstName = userDto.FirstName;
        LastName = userDto.LastName;
    }
}
