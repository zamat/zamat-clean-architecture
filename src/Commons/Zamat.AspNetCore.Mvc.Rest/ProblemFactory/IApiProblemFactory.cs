using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zamat.AspNetCore.Mvc.Rest.ProblemFactory;

public interface IApiProblemFactory
{
    ActionResult CreateValidationProblemResult(ModelStateDictionary modelState);
    ActionResult CreateProblemResult(string error);
}
