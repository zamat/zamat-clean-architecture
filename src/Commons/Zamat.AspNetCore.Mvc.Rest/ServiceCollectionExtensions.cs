using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json.Serialization;

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

                    logger.LogInformation("Invalid api request (errors : {errors})", errors);
                }

                return builtInFactory(context);
            };
        });

        return builder;
    }
}
