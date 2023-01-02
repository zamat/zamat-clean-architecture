namespace Zamat.AspNetCore.Multitenancy;

public class MultitenancyOptions
{
    public ResolvingStategy ResolvingStategy { get; private set; }
    public void UseHostResolver() => ResolvingStategy = ResolvingStategy.Host;
    public void UseHeaderResolver() => ResolvingStategy = ResolvingStategy.Header;
}
