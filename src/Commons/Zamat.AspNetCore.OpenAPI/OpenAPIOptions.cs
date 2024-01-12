using Microsoft.OpenApi.Models;

namespace Zamat.AspNetCore.OpenAPI;

public class OpenAPIOptions : OpenApiInfo
{
    public ICollection<OpenAPIEndpoint> Endpoints { get; set; } = new List<OpenAPIEndpoint>();
    public string AuthenticationScheme { get; set; } = string.Empty;
}
