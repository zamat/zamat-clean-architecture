using MMLib.Ocelot.Provider.AppConfiguration;
using Ocelot.DependencyInjection;

namespace Zamat.AspNetCore.Ocelot;

public static class OcelotBuilderExtensions
{
    public static IOcelotBuilder AddHostResolver(this IOcelotBuilder builder)
    {
        builder.AddAppConfiguration();

        return builder;
    }
}
