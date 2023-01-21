using Zamat.Sample.Services.Users.Api.Grpc.Client;

namespace Zamat.Sample.Services.Users.Api.Grpc.Tests.Services.v1.Users;

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
        var response = await client.GetUserAsync(new GetUserRequest() { Id = userId });
        #endregion

        #region Assert
        Assert.Equal(userId, response.Id);
        #endregion
    }
}
