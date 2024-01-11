using System.Linq.Expressions;

namespace Zamat.BuildingBlocks.Domain.Specifications;

public sealed class ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter = oldParameter;
    private readonly ParameterExpression _newParameter = newParameter;

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _oldParameter
            ? _newParameter
            : base.VisitParameter(node);
    }
}