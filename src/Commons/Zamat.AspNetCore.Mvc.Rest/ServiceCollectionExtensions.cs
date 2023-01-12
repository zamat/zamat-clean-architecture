using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Zamat.AspNetCore.Mvc.Rest.ProblemFactory;

namespace Zamat.AspNetCore.Mvc.Rest;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder ConfigureWebAPIMvc(this IMvcBuilder builder)
    {
        builder.Services.AddScoped<IApiProblemFactory, ApiProblemFactory>();

        builder.AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.ConfigureApiBehaviorOptions(o =>
        {
            o.DisableImplicitFromServicesParameters = true;
            o.InvalidModelStateResponseFactory = context =>
            {
                var problemFactory = context.HttpContext.RequestServices.GetRequiredService<IApiProblemFactory>();
                return problemFactory.CreateProblemResult(context.ModelState);
            };
        });

        return builder;
    }
}
