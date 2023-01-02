using System.Linq.Expressions;

namespace Zamat.Common.FilterQuery;

class BinaryExpressionVisitor : ExpressionVisitor
{
    public QueryParams QueryParams { get; }

    public BinaryExpressionVisitor()
    {
        QueryParams = new QueryParams();
    }
    protected override Expression VisitBinary(BinaryExpression node)
    {
        QueryParamOperator? queryParamOperator = node.NodeType switch
        {
            ExpressionType.Equal => QueryParamOperator.eq,
            ExpressionType.NotEqual => QueryParamOperator.noteq,
            ExpressionType.GreaterThan => QueryParamOperator.gt,
            ExpressionType.GreaterThanOrEqual => QueryParamOperator.gteq,
            ExpressionType.LessThan => QueryParamOperator.lt,
            ExpressionType.LessThanOrEqual => QueryParamOperator.lteq,
            _ => null,
        };

        if (queryParamOperator.HasValue)
        {
            string value = string.Empty;
            if (node.Right is ConstantExpression constantExpression)
                value = (string)constantExpression.Value!;
            var nodeLeft = node.Left.ToString();
            int lastDotPosition = nodeLeft.LastIndexOf('.') + 1;
            QueryParams.Add(new QueryParam(nodeLeft[lastDotPosition..], queryParamOperator.Value, value));
        }
        return base.VisitBinary(node);
    }
}
