using System.Collections.Generic;
using System.Text.Json;

namespace AUMS.AspNetCore.Opa;

public class DataSourceUpdateEvent
{
    public DataSourceUpdateEvent()
    {
        Entries = new List<DataSourceUpdateEntry>();
    }

    public List<DataSourceUpdateEntry> Entries { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
