namespace Zamat.Common.Multitenancy;

public class Tenant(string identifier)
{
    public string Identifier { get; } = identifier;
}
