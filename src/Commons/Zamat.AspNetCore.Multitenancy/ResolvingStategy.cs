namespace AUMS.AspNetCore.Multitenancy;

public enum ResolvingStategy
{
    Host,
    Header,
    Subdomain,
    Query,
    CookieOrQuery,
    PathBase
}
