using Microsoft.AspNetCore.Mvc;
using Zamat.Common.Command;

namespace Zamat.Sample.Services.Users.Api.Rest.ProblemDetails;

public interface IProblemFactory
{
    ActionResult CreateProblemResult(CommandResult commandResult);
}
