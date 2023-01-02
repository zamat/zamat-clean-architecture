namespace Zamat.Common.FilterQuery;

public record QueryParam(string Field, QueryParamOperator Operator, string Value)
{
}
