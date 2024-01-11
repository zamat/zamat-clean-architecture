namespace Zamat.Common.Crud;

public sealed record Result
{
    public List<Error> Errors { get; set; }
    public bool HasErrors => Errors.Count > 0;
    public bool Succeeded => !HasErrors;
    public void AddError(Error error) => Errors.Add(error);

    public Result()
    {
        Errors = new List<Error>();
    }

    public Result(Error error) : this()
    {
        Errors.Add(error);
    }

    public static implicit operator Result(List<Error> errors) => new(errors);
}

public abstract record Error(Enum ErrorCode, string ErrorMessage);
public record PreconditionError(Enum ErrorCode, string ErrorMessage) : Error(ErrorCode, ErrorMessage);
public record InfrastructureError(Enum ErrorCode, string ErrorMessage) : Error(ErrorCode, ErrorMessage);