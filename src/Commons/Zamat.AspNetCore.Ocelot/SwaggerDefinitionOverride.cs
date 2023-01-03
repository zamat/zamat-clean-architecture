using System.Collections.Generic;

namespace Zamat.AspNetCore.Ocelot;

public class SwaggerDefinitionOverride
{
    public IEnumerable<string> ParamsToRemove { get; private set; }

    public SwaggerDefinitionOverride()
    {
        ParamsToRemove = new List<string>();
    }

    public void RemoveParams(params string[] paramsToRemove) => ParamsToRemove = paramsToRemove;
}
