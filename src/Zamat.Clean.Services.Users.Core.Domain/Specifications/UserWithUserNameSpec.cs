using System.Linq.Expressions;
using Zamat.BuildingBlocks.Domain.Specifications;

namespace Zamat.Clean.Services.Users.Core.Domain.Specifications;

public class UserWithUserNameSpec(string userName) : Specification<User>
{
    private readonly string _userName = userName;

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.UserName == _userName;
    }
}
