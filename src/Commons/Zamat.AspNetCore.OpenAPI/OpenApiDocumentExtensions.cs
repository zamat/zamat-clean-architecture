using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Zamat.AspNetCore.OpenAPI;

public static class OpenApiDocumentExtensions
{
    public static OpenApiDocument RemoveParam(this OpenApiDocument doc, string paramName)
    {
        foreach (var path in doc.Paths)
        {
            path.Value.RemoveParam(paramName);
        }
        return doc;
    }

    public static OpenApiPathItem RemoveParam(this OpenApiPathItem path, string paramName)
    {
        foreach (var operation in path.Operations)
        {
            var parameters = operation.Value.Parameters.ToList();
            parameters.RemoveAll(x => x.Name.Equals(paramName, StringComparison.InvariantCultureIgnoreCase));
            operation.Value.Parameters = parameters;
        }
        return path;
    }
}
