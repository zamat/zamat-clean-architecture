using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using AUMS.Common.EmailSender;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.Smtp;

internal class SmtpSender : IEmailSender
{
    private readonly IOptions<SmtpOptions> _smtpOptions;

    private readonly ILogger<SmtpSender> _logger;

    public bool Enabled => _smtpOptions.Value.Enabled;

    public SmtpSender(IOptions<SmtpOptions> smtpOptions, ILogger<SmtpSender> logger)
    {
        _smtpOptions = smtpOptions;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, EmailDto emailData, CancellationToken cancellationToken = default)
    {
        var options = _smtpOptions.Value;
        if (!options.Enabled)
        {
            _logger.LogDebug("Sending email service is disabled.");
            return;
        }

        if (!options.Validate())
        {
            _logger.LogCritical("SmtpTransport configuration is not valid ({options})", options);

            throw new Exception("SmtpTransport configuration is not valid");
        }

        _logger.LogDebug("Sending email message (Recipient: {email}, Subject: {subject}, Body: Message:{body})", email, emailData.Subject, emailData.Body);

        using var smtpClient = new SmtpClient(options.Host, options.Port)
        {
            UseDefaultCredentials = options.UseDefaultCredentials,
            EnableSsl = options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = !string.IsNullOrEmpty(options.UserName) && !string.IsNullOrEmpty(options.Password) ? new NetworkCredential(options.UserName, options.Password) : null,
            Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds).Milliseconds
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(options.From),
            Subject = emailData.Subject,
            Body = emailData.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage, cancellationToken);
    }
}
