using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Zamat.AspNetCore.Mvc.Rest.ProblemFactory;

class ApiProblemFactory : IApiProblemFactory
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<ApiProblemFactory> _logger;

    const string ValidationError = "One or more validation errors occurred.";

    public ApiProblemFactory(ProblemDetailsFactory problemDetailsFactory, IHttpContextAccessor contextAccessor, ILogger<ApiProblemFactory> logger)
    {
        _problemDetailsFactory = problemDetailsFactory;
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public ActionResult CreateProblemResult(ModelStateDictionary modelState)
    {
        string errors = string.Join("|", modelState.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage));

        _logger.LogInformation("Invalid api model state (errors : {errors})", errors);

        var problemDetails = _problemDetailsFactory.CreateValidationProblemDetails(
            _contextAccessor.HttpContext!, 
            modelState, 
            StatusCodes.Status400BadRequest, 
            ValidationError, 
            instance: _contextAccessor.HttpContext?.Request.Path);

        return new BadRequestObjectResult(problemDetails);
    }
}
