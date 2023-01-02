using System;

namespace Zamat.Sample.BuildingBlocks.Core;

public interface IDateTimeService
{
    DateTimeOffset GetDateTimeOffsetUtcNow();
}

class DateTimeService : IDateTimeService
{
    public DateTimeOffset GetDateTimeOffsetUtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}
