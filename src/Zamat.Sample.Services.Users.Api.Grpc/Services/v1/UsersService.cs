using Grpc.Core;
using GrpcUsers;
using Zamat.Common.Query.Bus;
using Zamat.Sample.Services.Users.Core.Queries.Users;

namespace Zamat.Sample.Services.Users.Api.Grpc.Services.v1;

class UsersService : UsersSvc.UsersSvcBase
{
    private readonly IQueryBus _queryBus;
    private readonly ILogger<UsersService> _logger;

    public UsersService(IQueryBus queryBus, ILogger<UsersService> logger)
    {
        _queryBus = queryBus;
        _logger = logger;
    }

    public async override Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var query = await _queryBus.ExecuteAsync(new GetUserQuery(request.Id));
        if (!query.Succeeded)
        {
            _logger.LogWarning(UsersLogEvents.UserFetchError, "Get user problem ({Errors})", query.Errors);

            throw new RpcException(new Status(StatusCode.NotFound, "User with given id not found"));
        }

        return new UserReply()
        {
            Id = query.Result.Id,
            Firstname = query.Result.FirstName,
            Lastname = query.Result.LastName,
            Username = query.Result.UserName
        };
    }
}
