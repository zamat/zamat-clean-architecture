using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Zamat.AspNetCore.Localization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        var opt = new LocalizerOptions();
        configuration.GetSection(nameof(LocalizerOptions)).Bind(opt);

        if (!opt.SupportedCultures.Any())
        {
            throw new ArgumentException("SupportedCultures are required.");
        }
        if (string.IsNullOrEmpty(opt.DefaultCulture))
        {
            throw new ArgumentException("DefaultCulture is required.");
        }

        services.PostConfigure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(opt.DefaultCulture);
            options.SupportedCultures = opt.SupportedCultures.Select(x => new CultureInfo(x)).ToList();
            options.SupportedUICultures = opt.SupportedUICultures.Select(x => new CultureInfo(x)).ToList();
            options.ApplyCurrentCultureToResponseHeaders = opt.ApplyCurrentCultureToResponseHeaders;
            options.FallBackToParentCultures = opt.FallBackToParentCultures;
            options.FallBackToParentUICultures = opt.FallBackToParentUICultures;
        });

        services.Configure<LocalizationOptions>(options => options.ResourcesPath = opt.ResourcesPath);

        services.AddLocalization();

        return services;
    }
}
