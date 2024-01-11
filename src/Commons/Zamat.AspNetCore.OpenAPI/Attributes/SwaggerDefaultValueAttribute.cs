using System;

namespace Zamat.AspNetCore.OpenAPI.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerDefaultValueAttribute(string value) : Attribute
{
    public string Value { get; set; } = value;
}
