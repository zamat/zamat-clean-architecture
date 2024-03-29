﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Zamat.Sample.Services.Users.Api.Rest.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ApiErrorController : ControllerBase
{
    private readonly IStringLocalizer<Translations> _stringLocalizer;

    const string ErrorTitle = "Unexpected error occured.";
    const string ErrorDetail = "Operation cannot be performed. Contact with system admin.";

    public ApiErrorController(IStringLocalizer<Translations> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    [Route("api/error")]
    public ActionResult Get()
    {
        return Problem(
            detail: _stringLocalizer[ErrorDetail],
            title: _stringLocalizer[ErrorTitle]
            );
    }
}
