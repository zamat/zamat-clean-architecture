using Users.Api.V1;
using Zamat.Clean.Services.Users.Api.Grpc.IntegrationTests;
using Zamat.Clean.Services.Users.Api.Grpc.IntegrationTests.Services;

namespace Zamat.Clean.Services.Users.Api.Grpc.IntegrationTests.Services.v1.Users;

public class UsersServiceTests : BaseServiceTests
{
    public UsersServiceTests(UsersWebApplicationFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("u101")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string userId)
    {
        #region Arrange
        var client = new UsersSvc.UsersSvcClient(GrpcChannel);
        #endregion

        #region Act
        var response = await client.GetUserAsync(new GetUserRequest() { Id = userId }, deadline: DateTime.UtcNow.AddSeconds(5));
        #endregion

        #region Assert
        Assert.Equal(userId, response?.Id);
        #endregion
    }
}
