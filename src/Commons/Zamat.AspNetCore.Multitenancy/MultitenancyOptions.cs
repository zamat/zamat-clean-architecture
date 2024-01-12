namespace AUMS.AspNetCore.Multitenancy;

public class MultitenancyOptions
{
    public ResolvingStategy ResolvingStategy { get; private set; }
    public void UseHostResolver() => ResolvingStategy = ResolvingStategy.Host;
    public void UseHeaderResolver() => ResolvingStategy = ResolvingStategy.Header;
    public void UseSubdomainResolver() => ResolvingStategy = ResolvingStategy.Subdomain;
    public void UseQueryResolver() => ResolvingStategy = ResolvingStategy.Query;
    public void UseCookieOrQueryResolver() => ResolvingStategy = ResolvingStategy.CookieOrQuery;
    public void UsePathBaseResolver() => ResolvingStategy = ResolvingStategy.PathBase;
}
