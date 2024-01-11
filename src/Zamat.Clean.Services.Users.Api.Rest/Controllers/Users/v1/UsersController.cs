using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using Zamat.Clean.Services.Users.Api.Rest;
using Zamat.Clean.Services.Users.Api.Rest.Controllers;
using Zamat.Clean.Services.Users.Api.Rest.Controllers.Users.v1.ApiModel;
using Zamat.Common.Command;
using Zamat.Common.Query.Bus;
using Zamat.Clean.BuildingBlocks.Core;
using Zamat.Clean.Services.Users.Core.Commands.Users;
using Zamat.Clean.Services.Users.Core.Queries.Users;

namespace Zamat.Clean.Services.Users.Api.Rest.Controllers.Users.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/users")]
public class UsersController : ApiController
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IUuidGenerator _uuidGenerator;

    public UsersController(ICommandBus commandBus, IQueryBus queryBus, IUuidGenerator uuidGenerator, IStringLocalizer<Translations> stringLocalizer, ILogger<UsersController> logger) : base(stringLocalizer, logger)
    {
        _commandBus = commandBus;
        _queryBus = queryBus;
        _uuidGenerator = uuidGenerator;
    }

    [SwaggerOperation(
        Summary = "Create user",
        Description = "Create user",
        OperationId = "CreateUser",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(201, "The user was created")]
    [SwaggerResponse(204, "The user was not created")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateAsync(CreateUserRequest request)
    {
        var command = new CreateUserCommand(_uuidGenerator.Generate(), request.UserName, request.FirstName, request.LastName);

        var result = await _commandBus.ExecuteAsync(command);
        if (!result.Succeeded)
        {
            _logger.LogWarning(UsersLogEvents.UserCreated, "User cannot be created (errors: {errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(UsersLogEvents.UserCreated, "User created (userId : {Id})", command.Id);

        var query = await _queryBus.ExecuteAsync(new GetUserQuery(command.Id));
        if (!query.Succeeded)
        {
            _logger.LogWarning(UsersLogEvents.UserFetchError, "Get user problem ({Errors})", query.Errors);
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
            _logger.LogWarning(UsersLogEvents.UserFetchError, "Get user problem ({Errors})", count.Errors);
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
            _logger.LogWarning(UsersLogEvents.UserFetchError, "Get user problem ({Errors})", query.Errors);
            return NoContent();
        }

        return Ok(new GetUserResponse(query.Result));
    }

    [SwaggerOperation(
        Summary = "Delete user",
        Description = "Delete user",
        OperationId = "DeleteUser",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(204, "The user entity was deleted.")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        var command = new DeleteUserCommand(id);

        var result = await _commandBus.ExecuteAsync(command);
        if (!result.Succeeded)
        {
            _logger.LogWarning(UsersLogEvents.UserDeleted, "User cannot be deleted (errors: {errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(UsersLogEvents.UserDeleted, "User was deleted (userId : {Id})", command.Id);

        return NoContent();
    }

    [SwaggerOperation(
        Summary = "Update user",
        Description = "Update user",
        OperationId = "UpdateUser",
        Tags = new[] { "Users" }
    )]
    [SwaggerResponse(204, "The user entity was updated.")]
    [SwaggerResponse(400, "Api problem occured")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CreateUserResponse>> UpdateAsync(string id, UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(id, request.FirstName, request.LastName);

        var result = await _commandBus.ExecuteAsync(command);
        if (!result.Succeeded)
        {
            _logger.LogWarning(UsersLogEvents.UserUpdated, "User cannot be updated (errors: {errors})", result.Errors);

            return ProblemDetailsResult(result);
        }

        _logger.LogInformation(UsersLogEvents.UserUpdated, "User updated (userId : {Id}, command: {command})", command.Id, command);

        return NoContent();
    }
}
