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

        var query = await _queryBus.ExecuteAsync(new GetUserQuery(command.Id));
        if (!query.Succeeded)
        {
            _logger.LogInformation("Get user problem ({Errors})", query.Errors);
            return NoContent();
        }

        return CreatedAtRoute("GetUser", new { command.Id }, new CreateUserResponse(query.Result));
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
    public async Task<ActionResult<CreateUserResponse>> GetAsync(string id)
    {
        var query = await _queryBus.ExecuteAsync(new GetUserQuery(id));
        if (!query.Succeeded)
        {
            _logger.LogInformation("Get user problem ({Errors})", query.Errors);
            return NoContent();
        }

        return Ok(new CreateUserResponse(query.Result));
    }
}
