using System.Collections.Generic;

namespace Zamat.AspNetCore.Ocelot;
public class SwaggerDefinition
{
    public IEnumerable<string> ParamsToRemove { get; private set; }

    public SwaggerDefinition()
    {
        ParamsToRemove = new List<string>();
    }

    public void RemoveParams(params string[] paramsToRemove) => ParamsToRemove = paramsToRemove;
}
