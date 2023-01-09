using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zamat.Common.Command;
using Zamat.Common.Query.Bus;
using Zamat.Sample.BuildingBlocks.Core;
using Zamat.Sample.Services.Users.Api.Rest.ProblemDetails;
using Zamat.Sample.Services.Users.Core.Commands.Users;
using Zamat.Sample.Services.Users.Core.Queries.Users;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers.Users.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IProblemFactory _problemFactory;
    private readonly IUuidGenerator _uuidGenerator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ICommandBus commandBus, IQueryBus queryBus, IProblemFactory problemFactory, IUuidGenerator uuidGenerator, ILogger<UsersController> logger)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _problemFactory = problemFactory;
        _uuidGenerator = uuidGenerator;
        _logger = logger;
    }

    [SwaggerOperation(
        Summary = "Create user",
        Description = "Create user",
        OperationId = "CreateUser",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(201, "The user was created")]
    [SwaggerResponse(204, "The user was not created")]
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(CreateUserRequest request)
    {
        var command = new CreateUserCommand(_uuidGenerator.Generate(), request.UserName, request.FirstName, request.LastName);

        var result = await _commandBus.ExecuteAsync(command);
        if (!result.Succeeded)
        {
            return _problemFactory.CreateProblemResult(result);
        }

        _logger.LogInformation("User created (userId : {Id})", command.Id);

        var query = await _queryBus.ExecuteAsync(new GetUserQuery(command.Id));
        if (!query.Succeeded)
        {
            _logger.LogInformation("Get user problem ({Errors})", query.Errors);
            return NoContent();
        }

        return CreatedAtRoute("GetUser", new { command.Id }, new CreateUserResponse(query.Result));
    }

    [SwaggerOperation(
        Summary = "Get users paginable list",
        Description = "Get users paginable list",
        OperationId = "GetUsersPaginableList",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(200, "The users paginator")]
    [HttpGet]
    public async Task<ActionResult<GetUsersResponse>> GetAsync([FromQuery] GetUsersRequest request)
    {
        var count = await _queryBus.ExecuteAsync(new GetUsersCountQuery());
        if (!count.Succeeded)
        {
            _logger.LogWarning("Get user problem ({Errors})", count.Errors);
            return GetEmptyResult();
        }
        if (count.Result <= 0)
        {
            return GetEmptyResult();
        }

        var users = await _queryBus.ExecuteAsync(new GetUsersQuery(request.Page, request.Limit));

        var items = new List<GetUserResponse>();

        foreach (var user in users.Result)
        {
            items.Add(new GetUserResponse(user));
        }

        return Ok(new GetUsersResponse(request.Page, request.Limit, count.Result, items));

        GetUsersResponse GetEmptyResult() => new(request.Page, request.Limit);
    }

    [SwaggerOperation(
        Summary = "Get user",
        Description = "Get user",
        OperationId = "GetUser",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(200, "The user entity")]
    [SwaggerResponse(204, "The user entity not found")]
    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<GetUserResponse>> GetAsync(string id)
    {
        var query = await _queryBus.ExecuteAsync(new GetUserQuery(id));
        if (!query.Succeeded)
        {
            _logger.LogInformation("Get user problem ({Errors})", query.Errors);
            return NoContent();
        }

        return Ok(new GetUserResponse(query.Result));
    }
}
