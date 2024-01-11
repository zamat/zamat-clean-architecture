using System.Linq.Expressions;
using Zamat.BuildingBlocks.Domain.Specifications;

namespace Zamat.Clean.Services.Users.Core.Domain.Specifications;

public class UserWithIdSpec(string id) : Specification<User>
{
    private readonly string _id = id;

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.Id == _id;
    }
}
