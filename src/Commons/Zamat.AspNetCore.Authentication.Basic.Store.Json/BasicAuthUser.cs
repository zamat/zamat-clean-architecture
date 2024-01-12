namespace AUMS.AspNetCore.Authentication.Basic.Store.Json;

internal record BasicAuthUser(string UserName, string Password, IEnumerable<BasicAuthUserClaim> Claims)
{
}
