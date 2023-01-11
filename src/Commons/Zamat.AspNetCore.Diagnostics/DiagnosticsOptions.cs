namespace Zamat.AspNetCore.Diagnostics;

public class DiagnosticsOptions
{
    public bool UseTraceIdResponseHeader { get; private set; } = true;
    public void AddTraceIdToResponseHeaders() => UseTraceIdResponseHeader = true;
}
