using System.Net;
using Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

namespace Zamat.Sample.Services.Users.Api.Rest.Tests.Controllers.v1.Users;

public class UsersControllerTests : IClassFixture<UsersWebApplicationFactory>
{
    private readonly UsersWebApplicationFactory _factory;

    public UsersControllerTests(UsersWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/v1/users/u100")]
    [InlineData("/v1/users/u101")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        #region Arrange
        var client = _factory.CreateClient();
        #endregion

        #region Act
        var response = await client.GetAsync(url);
        #endregion

        #region Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.OK);
        #endregion
    }

    [Theory]
    [InlineData("jack.rush@contoso.com", "Jack", "Rush")]
    [InlineData("sily.rush@contoso.com", "Siri", "Rush")]
    public async Task Post_EndpointsReturnSuccessAndCorrectContentType(string userName, string firstName, string lastName)
    {
        #region Arrange
        var client = _factory.CreateClient();
        #endregion

        #region Act
        var user = new CreateUserRequest()
        {
            UserName = userName,
            FirstName = firstName,
            LastName = lastName
        };
        var response = await client.PostAsJsonAsync("/v1/users", user);
        #endregion

        #region Assert
        response.EnsureSuccessStatusCode();
        Assert.True(response.StatusCode is HttpStatusCode.Created);
        #endregion
    }

    [Theory]
    [InlineData("john.doe@constoso.com", "John", "Doe")]
    public async Task Post_EndpointsReturnBadRequestAndCorrectContentType(string userName, string firstName, string lastName)
    {
        #region Arrange
        var client = _factory.CreateClient();
        #endregion

        #region Act
        var user = new CreateUserRequest()
        {
            UserName = userName,
            FirstName = firstName,
            LastName = lastName
        };
        var response = await client.PostAsJsonAsync("/v1/users", user);
        #endregion

        #region Assert
        Assert.True(response.StatusCode is HttpStatusCode.BadRequest);
        Assert.Equal("application/problem+json; charset=utf-8", $"{response.Content.Headers.ContentType}");
        #endregion
    }
}
