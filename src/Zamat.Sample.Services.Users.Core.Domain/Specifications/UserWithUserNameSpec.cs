﻿using System.Linq.Expressions;
using Zamat.BuildingBlocks.Domain.Specifications;

namespace Zamat.Sample.Services.Users.Core.Domain.Specifications;

public class UserWithUserNameSpec : Specification<User>
{
    private readonly string _userName;

    public UserWithUserNameSpec(string userName)
    {
        _userName = userName;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return x => x.UserName == _userName;
    }
}
