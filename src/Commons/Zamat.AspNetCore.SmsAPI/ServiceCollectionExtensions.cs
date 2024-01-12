using System;
using AUMS.Common.SmsSender;
using Microsoft.Extensions.DependencyInjection;

namespace AUMS.AspNetCore.SmsAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSmsSender(this IServiceCollection services, Action<SmsAPIOptions> options)
    {
        services.Configure(options);

        services.AddSingleton<ISmsSender, SmsAPISender>();

        return services;
    }
}
