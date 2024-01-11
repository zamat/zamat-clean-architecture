using Microsoft.OpenApi.Models;

namespace Zamat.AspNetCore.OpenAPI;

public class OpenAPIOptions : OpenApiInfo
{
    public List<OpenAPIEndpoint> Endpoints { get; set; } = new List<OpenAPIEndpoint>();
}