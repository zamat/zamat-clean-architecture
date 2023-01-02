namespace Zamat.Common.Multitenancy;

public class Tenant
{
    public string Identifier { get; }

    public Tenant(string identifier)
    {
        Identifier = identifier;
    }
}
