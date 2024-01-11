using Microsoft.AspNetCore.Builder;

namespace Zamat.AspNetCore.Multitenancy;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder, Action<MultitenancyOptions> configure)
    {
        var opt = new MultitenancyOptions();
        configure(opt);

        _ = opt.ResolvingStategy switch
        {
            ResolvingStategy.Host => builder.UseMiddleware<HostMiddleware>(),
            ResolvingStategy.Header => builder.UseMiddleware<HeaderMiddleware>(),
            _ => throw new NotImplementedException(),
        };

        return builder;
    }
}
