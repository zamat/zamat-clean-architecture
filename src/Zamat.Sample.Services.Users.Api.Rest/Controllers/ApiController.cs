using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zamat.Common.Command;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers;

public abstract class ApiController : ControllerBase
{
    protected ActionResult ProblemDetailsResult(CommandResult commandResult)
    {
        if (commandResult.IsDomainProblem)
        {
            var error = commandResult.Errors.First(e => e is DomainError);
            return Problem(title: error.ErrorMessage);
        }
        else
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in commandResult.Errors)
            {
                (string key, string value) = error.ErrorCode switch
                {
                    _ => ($"{error.ErrorCode}", error.ErrorMessage)
                };
                modelState.AddModelError(key, value);
            }

            return ValidationProblem(
                title: "One or more validation errors occurred.",
                modelStateDictionary: modelState,
                statusCode: StatusCodes.Status400BadRequest
               );
        }
    }
}
