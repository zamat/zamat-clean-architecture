using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Zamat.AspNetCore.Mvc.Rest;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder ConfigureWebAPIMvc(this IMvcBuilder builder)
    {
        builder.Services.AddTransient<ProblemDetailsFactory, ConcreteProblemDetailsFactory>();

        builder.AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.ConfigureApiBehaviorOptions(o =>
        {
            o.DisableImplicitFromServicesParameters = true;
        });

        return builder;
    }
}
