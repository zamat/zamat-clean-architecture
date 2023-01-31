using Grpc.Net.Client;

namespace Zamat.Sample.Services.Users.Api.Grpc.IntegrationTests.Services;

public class BaseServiceTests : IClassFixture<UsersWebApplicationFactory>
{
    private readonly UsersWebApplicationFactory _factory;
    public GrpcChannel GrpcChannel
    {
        get
        {
            var client = _factory.CreateDefaultClient();
            return GrpcChannel.ForAddress(client.BaseAddress!, new GrpcChannelOptions
            {
                HttpClient = client
            });
        }
    }

    public BaseServiceTests(UsersWebApplicationFactory factory)
    {
        _factory = factory;
    }
}
