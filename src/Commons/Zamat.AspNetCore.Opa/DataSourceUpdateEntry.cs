using System.Collections.Generic;

namespace AUMS.AspNetCore.Opa;

public class DataSourceUpdateEntry
{
    public string Url { get; set; } = default!;

    public List<string> Topics { get; set; } = new List<string>();

    public string DsnPath { get; set; } = default!;

    public string SaveMethod { get; set; } = default!;
}
