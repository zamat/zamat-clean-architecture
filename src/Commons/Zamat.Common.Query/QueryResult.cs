namespace Zamat.Common.Query;

public record QueryResult<T>
{
    public QueryResult()
    {
        Errors = [];
    }

    public QueryResult(QueryError error) : this()
    {
        Errors.Add(error);
    }

    public T Result { get; init; } = default!;

    public List<QueryError> Errors { get; set; }

    public bool HasErrors => Errors.Count > 0;

    public bool Succeeded => !HasErrors;

    public void AddError(Enum errorCode, string errorMessage) => Errors.Add(new QueryError(errorCode, errorMessage));

    public static implicit operator QueryResult<T>(List<QueryError> errors) => new(errors);

    public static implicit operator QueryResult<T>(T result) => new() { Result = result };
}
