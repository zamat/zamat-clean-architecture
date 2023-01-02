using System.Collections.Generic;
using System.Text.Json;

namespace Zamat.Common.ProblemDetails;

public class Problem
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
    public int? Status { get; set; }
    public Dictionary<string, object>? Extensions { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
