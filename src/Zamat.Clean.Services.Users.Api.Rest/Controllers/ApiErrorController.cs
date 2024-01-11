using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Zamat.Clean.Services.Users.Api.Rest.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ApiErrorController(IStringLocalizer<Translations> stringLocalizer) : ControllerBase
{
    private readonly IStringLocalizer<Translations> _stringLocalizer = stringLocalizer;
    private const string ErrorTitle = "Unexpected error occured.";
    private const string ErrorDetail = "Operation cannot be performed. Contact with system admin.";

    [Route("api/error")]
    public ActionResult Get()
    {
        return Problem(
            detail: _stringLocalizer[ErrorDetail],
            title: _stringLocalizer[ErrorTitle]
            );
    }
}
