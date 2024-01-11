using System.Linq.Expressions;
using Zamat.BuildingBlocks.Domain.Specifications;

namespace Zamat.Clean.Services.Users.Core.Domain.Specifications;

public class UserWithIdSpec : Specification<User>
{
    private readonly string _id;

    public UserWithIdSpec(string id)
    {
        _id = id;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.Id == _id;
    }
}
