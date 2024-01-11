using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Zamat.AspNetCore.Diagnostics.HealthChecks;

internal record HealthCheckResponse(HealthStatus Status)
{
    public Dictionary<string, HealthCheckEntryResponse> Entries { get; } = new();
    public void Add(string key, HealthCheckEntryResponse entry) => Entries.Add(key, entry);
}
