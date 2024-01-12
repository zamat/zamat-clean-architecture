using System.Collections.Generic;

namespace AUMS.AspNetCore.BackendForFrontend;

internal record UserInfoResponse(bool IsAuthenticated, UserInfo? UserInfo, IEnumerable<string> Permissions)
{
}
