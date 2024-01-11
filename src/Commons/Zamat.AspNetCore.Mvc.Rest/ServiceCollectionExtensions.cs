using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Zamat.AspNetCore.Mvc.Rest;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder ConfigureWebAPIMvc(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.ConfigureApiBehaviorOptions(o =>
        {
            o.DisableImplicitFromServicesParameters = true;
            var builtInFactory = o.InvalidModelStateResponseFactory;

            o.InvalidModelStateResponseFactory = context =>
            {
                if (!context.ModelState.IsValid)
                {
                    var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("Default");

                    string errors = string.Join("|", context.ModelState.Values
                        .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage));

                    var traceId = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier;
                    logger.LogInformation("Invalid api request (errors : {errors}, traceId: {traceId})", errors, traceId);
                }

                return builtInFactory(context);
            };
        });

        return builder;
    }
}
