namespace Zamat.Common.OpenTelemetry;

public class OpenTelemetryServiceOptions
{
    public string OtlpEndpoint { get; set; } = default!;
    public string ServiceName { get; set; } = default!;
    public string ServiceNamespace { get; set; } = default!;
    public string ServiceVersion { get; set; } = default!;
    public string ServiceInstanceId { get; set; } = default!;
    public bool AutoGenerateServiceInstanceId { get; set; } = true;
    public bool Enabled { get; set; } = true;
}
