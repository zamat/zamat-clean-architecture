using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Zamat.Common.Command;
using Zamat.Sample.Services.Users.Core.Commands;

namespace Zamat.Sample.Services.Users.Api.Rest.ProblemDetails;

class ProblemFactory : IProblemFactory
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IStringLocalizer<Translations> _stringLocalizer;

    const string ValidationError = "One or more validation errors occurred.";

    public ProblemFactory(ProblemDetailsFactory problemDetailsFactory, IHttpContextAccessor contextAccessor, IStringLocalizer<Translations> stringLocalizer)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _contextAccessor = contextAccessor;
        _stringLocalizer = stringLocalizer;
    }

    public ActionResult CreateProblemResult(CommandResult commandResult)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in commandResult.Errors)
        {
            (string key, string value) = error.ErrorCode switch
            {
                CommandErrorCode.UserNameNotUnique => ("userName", _stringLocalizer[error.ErrorMessage]),
                _ => ($"{error.ErrorCode}", _stringLocalizer[error.ErrorMessage])
            };
            modelState.AddModelError(key, value);
        }

        var problemDetails = _problemDetailsFactory.CreateValidationProblemDetails(_contextAccessor.HttpContext!, modelState, StatusCodes.Status400BadRequest, _stringLocalizer[ValidationError]);

        return new BadRequestObjectResult(problemDetails);
    }
}
