using Ocelot.DependencyInjection;

namespace Zamat.Sample.ApiGateway.DelegatingHandlers;

public static class OcelotBuilderExtensions
{
    public static IOcelotBuilder AddDelegatingHandlers(this IOcelotBuilder builder)
    {
        builder.AddDelegatingHandler<UsersDelegatingHandler>();
        return builder;
    }
}
