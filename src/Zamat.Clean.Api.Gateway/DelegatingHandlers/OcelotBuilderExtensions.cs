using Ocelot.DependencyInjection;

namespace Zamat.Clean.Api.Gateway.DelegatingHandlers;

public static class OcelotBuilderExtensions
{
    public static IOcelotBuilder AddDelegatingHandlers(this IOcelotBuilder builder)
    {
        builder.AddDelegatingHandler<UsersDelegatingHandler>();
        return builder;
    }
}
