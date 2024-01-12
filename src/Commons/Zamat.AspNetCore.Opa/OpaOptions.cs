namespace AUMS.AspNetCore.Opa;

public class OpaOptions
{
    public int Timeout { get; set; } = 1000;
    public string BaseAddress { get; set; } = string.Empty;
}
