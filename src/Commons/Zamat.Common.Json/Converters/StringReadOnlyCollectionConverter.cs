using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AUMS.Common.Json.Converters;

public class StringReadOnlyCollectionConverter : JsonConverter<ReadOnlyCollection<string>>
{
    public override ReadOnlyCollection<String> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
    {
        var values = new List<string>();
        var jsonObject = JsonDocument.ParseValue(ref reader);
        if (jsonObject is null)
        {
            return new ReadOnlyCollection<string>(Array.Empty<string>());
        }

        try
        {
            values = JsonSerializer.Deserialize<List<string>>(jsonObject.RootElement.GetRawText(), options);
        }
        catch (JsonException ex)
        {
        }

        return new ReadOnlyCollection<string>(values);
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReadOnlyCollection<String> value,
        JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value)
        {
            writer.WriteStringValue(item);
        }

        writer.WriteEndArray();
    }
}
