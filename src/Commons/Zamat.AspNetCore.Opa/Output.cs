using System.Text.Json.Serialization;

namespace AUMS.AspNetCore.Opa;

internal class Output
{
    [JsonPropertyName("result")]
    public bool Result { get; set; }

    [JsonPropertyName("decision_id")]
    public string? DecisionId { get; set; }

    public static Output Success => new() { Result = true };

    public static Output Fail => new() { Result = false };
}
