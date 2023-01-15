using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;

namespace Zamat.AspNetCore.Mvc.Rest;

class ConcreteProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly Action<ProblemDetailsContext>? _configure;
    private readonly ILogger<ConcreteProblemDetailsFactory>? _logger;

    public ConcreteProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null,
        ILogger<ConcreteProblemDetailsFactory>? logger = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
        _logger = logger;
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode ?? 500,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance ?? httpContext.Request.Path,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode ?? 500);

        _logger?.LogInformation("Create api problem ([title: {title}], [detail: {detail}], [type: {type}], [instance: {instance}]))", problemDetails.Title, problemDetails.Detail, problemDetails.Type, problemDetails.Instance);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        string errors = string.Join("|", modelStateDictionary.Values.SelectMany(state => state.Errors).Select(error => error.ErrorMessage));

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode ?? 500,
            Type = type,
            Detail = detail,
            Instance = instance ?? httpContext.Request.Path,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode ?? 500);

        _logger?.LogInformation("Create validation api problem ([title: {title}], [type: {type}], [instance: {instance}], [errors: {errors}]))", problemDetails.Title, problemDetails.Type, problemDetails.Instance, errors);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        _configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });
    }
}
