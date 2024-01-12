namespace Zamat.AspNetCore.OpenAPI.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerDefaultValueAttribute : Attribute
{
    public SwaggerDefaultValueAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; set; }
}
