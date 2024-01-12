using System.Collections.Generic;

namespace AUMS.AspNetCore.Opa;

internal class Request
{
    public string? Protocol { get; set; }

    public string? Host { get; set; }

    public string? Path { get; set; }

    public string? PathBase { get; set; }

    public string? Scheme { get; set; }

    public string? Method { get; set; }

    public string? QueryString { get; set; }

    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

    public string? TraceId { get; set; }
}
