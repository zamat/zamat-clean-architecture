namespace AUMS.AspNetCore.Opa;

internal class Input
{
    public Request? Request { get; set; }

    public Connection? Connection { get; set; }

    public string User { get; set; } = default!;

    public string Feature { get; set; } = default!;
}
