using MMLib.SwaggerForOcelot.Configuration;

namespace Zamat.AspNetCore.Ocelot;

public class SwaggerDefinitionOverride
{
    public IEnumerable<string> ParamsToRemove { get; private set; }
    public Action<OcelotGatewayItSelfSwaggerGenOptions>? GatewaySwaggerGen { get; set; }

    public SwaggerDefinitionOverride()
    {
        ParamsToRemove = new List<string>();
    }

    public void RemoveParams(params string[] paramsToRemove) => ParamsToRemove = paramsToRemove;
}
