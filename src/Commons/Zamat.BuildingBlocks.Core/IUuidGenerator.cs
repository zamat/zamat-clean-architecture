namespace Zamat.BuildingBlocks.Core;

public interface IUuidGenerator
{
    string Generate();
}

internal class UuidGenerator : IUuidGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
