using System.Linq.Expressions;

namespace Zamat.BuildingBlocks.Domain.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public bool IsSatisfied(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public Specification<T> And(Specification<T> specification)
        => new AndSpecification<T>(this, specification);

    public Specification<T> Or(Specification<T> specification)
        => new OrSpecification<T>(this, specification);
}