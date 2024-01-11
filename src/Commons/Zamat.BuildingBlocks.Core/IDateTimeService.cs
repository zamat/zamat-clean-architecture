namespace Zamat.BuildingBlocks.Core;

public interface IDateTimeService
{
    DateTimeOffset GetDateTimeOffsetUtcNow();
}

internal class DateTimeService : IDateTimeService
{
    public DateTimeOffset GetDateTimeOffsetUtcNow()
    {
        return DateTimeOffset.UtcNow;
    }
}
