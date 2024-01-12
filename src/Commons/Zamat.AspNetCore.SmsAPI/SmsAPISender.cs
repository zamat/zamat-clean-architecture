using System.Threading;
using System.Threading.Tasks;
using AUMS.Common.SmsSender;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SMSApi.Api;

namespace AUMS.AspNetCore.SmsAPI;

internal class SmsAPISender : ISmsSender
{
    private readonly IOptions<SmsAPIOptions> _smsApiOptions;

    private readonly ILogger<SmsAPISender> _logger;

    public SmsAPISender(IOptions<SmsAPIOptions> smsApiOptions, ILogger<SmsAPISender> logger)
    {
        _smsApiOptions = smsApiOptions;
        _logger = logger;
    }

    public bool Enabled => _smsApiOptions.Value.Enabled;

    public Task SendSmsAsync(string phoneNumber, string text, CancellationToken cancellationToken = default)
    {
        var options = _smsApiOptions.Value;

        if (!options.Enabled)
        {
            _logger.LogDebug("SMSAPI service is disabled");

            return Task.CompletedTask;
        }

        if (!options.Validate())
        {
            _logger.LogCritical("SMSAPI configuration is not valid ({options})", options);

            throw new Exception("SMSAPI configuration is not valid");
        }

        _logger.LogDebug("Sending sms message (Phone: {phoneNumber}, Text: {text}", phoneNumber, text);

        var client = new ClientOAuth(options.Token);
        var smsApi = new SMSFactory(client);

        _ = smsApi
            .ActionSend()
            .SetText(text)
            .SetTo(phoneNumber)
            .SetSender(options.SenderName)
            .Execute();

        return Task.CompletedTask;
    }
}
