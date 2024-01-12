using System;
using AUMS.Common.EmailSender;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.Smtp;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSmtpSender(this IServiceCollection services, Action<SmtpOptions> options)
    {
        services.Configure(options);

        services.AddSingleton<IEmailSender, SmtpSender>();

        return services;
    }
}
