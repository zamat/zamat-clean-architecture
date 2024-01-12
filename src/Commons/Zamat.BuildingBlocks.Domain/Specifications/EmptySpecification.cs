using System.Linq.Expressions;

namespace Zamat.BuildingBlocks.Domain.Specifications;

public class EmptySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}
