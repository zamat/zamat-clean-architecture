using Microsoft.Extensions.Logging.Console;

namespace AUMS.Common.Logging.Formatters;

public sealed class CloudConsoleFormatterOptions : ConsoleFormatterOptions
{
    public bool SingleLine { get; set; } = true;
    public string DateFormat { get; set; } = "o";
    public string HeaderFormat { get; set; } = "{date} {version} {logLevel}: ";
    public string HeaderDetailsFormat { get; set; } = "{logCategory}[{logEventId}]";
}
