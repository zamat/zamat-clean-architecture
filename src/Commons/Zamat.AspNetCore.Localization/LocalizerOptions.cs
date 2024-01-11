using Microsoft.Extensions.Localization;

namespace Zamat.AspNetCore.Localization;

public class LocalizerOptions : LocalizationOptions
{
    public string DefaultCulture { get; set; } = default!;
    public bool ApplyCurrentCultureToResponseHeaders { get; set; } = true;
    public bool FallBackToParentCultures { get; set; } = true;
    public bool FallBackToParentUICultures { get; set; } = true;
    public List<string> SupportedCultures { get; set; } = new List<string>();
    public List<string> SupportedUICultures { get; set; } = new List<string>();
}
