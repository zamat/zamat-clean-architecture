using AUMS.Common.Logging.Formatters;
using Microsoft.Extensions.Logging;

namespace AUMS.Common.Logging;

public static class ILoggingBuilderExtensions
{
    public static ILoggingBuilder AddCloudConsoleFormatter(this ILoggingBuilder builder)
    {
        builder.AddConsoleFormatter<CloudConsoleFormatter, CloudConsoleFormatterOptions>();

        return builder;
    }
}
