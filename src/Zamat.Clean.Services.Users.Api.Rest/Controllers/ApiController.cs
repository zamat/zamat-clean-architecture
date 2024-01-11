using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Zamat.Common.Command;

namespace Zamat.Clean.Services.Users.Api.Rest.Controllers;

public abstract class ApiController : ControllerBase
{
    const string ValidationError = "One or more validation errors occurred.";

    protected readonly IStringLocalizer _stringLocalizer;
    protected readonly ILogger _logger;

    protected ApiController(IStringLocalizer stringLocalizer, ILogger logger)
    {
        _stringLocalizer = stringLocalizer;
        _logger = logger;
    }

    protected ActionResult ProblemDetailsResult(CommandResult commandResult)
    {
        if (commandResult.IsDomainProblem)
        {
            var error = commandResult.Errors.First(e => e is DomainError);
            return Problem(title: _stringLocalizer[error.ErrorMessage], detail: _stringLocalizer[$"{error.ErrorCode}"], statusCode: StatusCodes.Status400BadRequest);
        }

        foreach (var error in commandResult.Errors)
        {
            (string key, string value) = error.ErrorCode switch
            {
                _ => (_stringLocalizer[$"{error.ErrorCode}"], _stringLocalizer[error.ErrorMessage])
            };
            ModelState.AddModelError(key, value);
        }
        return ValidationProblem(title: _stringLocalizer[ValidationError], modelStateDictionary: ModelState, statusCode: StatusCodes.Status400BadRequest);
    }
}
