using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace AUMS.Common.Logging.Formatters;

public sealed class CloudConsoleFormatter : ConsoleFormatter, IDisposable
{
    private readonly IDisposable? _optionsReloadToken;

    private CloudConsoleFormatterOptions _formatterOptions;
    private readonly string _version = Environment.GetEnvironmentVariable("VERSION_NUMBER") ?? "XXX";

    public CloudConsoleFormatter(IOptionsMonitor<CloudConsoleFormatterOptions> options) : base(nameof(CloudConsoleFormatter))
    {
        (_optionsReloadToken, _formatterOptions) =
        (options.OnChange(ReloadLoggerOptions), options.CurrentValue);
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);
        Exception? exception = logEntry.Exception;

        if (logEntry.Exception == null && message == null)
        {
            return;
        }

        if (message == null)
        {
            return;
        }

        var header = FormatText(logEntry, _formatterOptions.HeaderFormat);
        var headerDetails = FormatText(logEntry, _formatterOptions.HeaderDetailsFormat);

        textWriter.Write($"{header}{headerDetails}");
        WriteLine(textWriter);

        WriteScopeInformation(textWriter, scopeProvider, _formatterOptions.SingleLine, header.Length);
        WriteMessage(textWriter, message, _formatterOptions.SingleLine, header.Length);
        WriteException(textWriter, exception, _formatterOptions.SingleLine, header.Length);

        textWriter.Write(Environment.NewLine);
    }

    public string FormatText<TState>(in LogEntry<TState> logEntry, string textFormat)
    {
        var parameters = new Dictionary<string, string>
        {
            { @"{date}", DateTime.UtcNow.ToString(_formatterOptions.DateFormat) },
            { @"{version}", _version },
            { @"{logLevel}", GetLogLevelString(logEntry.LogLevel) },
            { @"{logCategory}", logEntry.Category },
            { @"{logEventId}", logEntry.EventId.Id.ToString() }
        };

        return parameters.Aggregate(textFormat, (s, kv) => s.Replace(kv.Key, kv.Value));
    }

    public void WriteLine(TextWriter textWriter)
    {
        if (!_formatterOptions.SingleLine)
        {
            textWriter.Write(Environment.NewLine);
        }
    }

    public void Dispose() => _optionsReloadToken?.Dispose();

    private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider? scopeProvider, bool singleLine, int paddingLength)
    {
        if (_formatterOptions.IncludeScopes && scopeProvider != null)
        {
            bool paddingNeeded = !singleLine;
            scopeProvider.ForEachScope((scope, state) =>
            {
                if (paddingNeeded)
                {
                    paddingNeeded = false;
                    state.Write($"{MessagePadding(paddingLength)}=> ");
                }
                else
                {
                    state.Write(" => ");
                }

                state.Write(scope);
            }, textWriter);

            if (!paddingNeeded && !singleLine)
            {
                textWriter.Write(Environment.NewLine);
            }
        }
    }

    private static void WriteMessage(TextWriter textWriter, string? message, bool singleLine, int paddingLength)
    {
        if (!string.IsNullOrEmpty(message))
        {
            if (singleLine)
            {
                textWriter.Write($" {message.Replace(Environment.NewLine, " ")}");
            }
            else
            {
                textWriter.Write($"{MessagePadding(paddingLength)}{message.Replace(Environment.NewLine, Environment.NewLine + MessagePadding(paddingLength))}");
                textWriter.Write(Environment.NewLine);
            }
        }
    }

    private static void WriteException(TextWriter textWriter, Exception? exception, bool singleLine, int paddingLength)
    {
        if (exception is not null)
        {
            WriteMessage(textWriter, exception.ToString(), singleLine, paddingLength);
        }
    }

    private void ReloadLoggerOptions(CloudConsoleFormatterOptions options)
    {
        _formatterOptions = options;
    }

    private static string MessagePadding(int length) => new(' ', length);

    private static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "TRAC",
            LogLevel.Debug => "DBUG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Critical => "CRIT",
            LogLevel.Error => "FAIL",
            LogLevel.None => "NONE",
            _ => string.Empty,
        };
    }
}
