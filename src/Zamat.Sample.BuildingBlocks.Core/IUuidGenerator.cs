using System;

namespace Zamat.Sample.BuildingBlocks.Core;

public interface IUuidGenerator
{
    string Generate();
}

class UuidGenerator : IUuidGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
