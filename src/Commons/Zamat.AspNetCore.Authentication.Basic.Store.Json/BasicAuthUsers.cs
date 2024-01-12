namespace AUMS.AspNetCore.Authentication.Basic.Store.Json;

internal record BasicAuthUsers(IEnumerable<BasicAuthUser> Users)
{
}
