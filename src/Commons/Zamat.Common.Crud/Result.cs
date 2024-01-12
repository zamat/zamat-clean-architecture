namespace Zamat.Common.Crud;

public sealed record Result
{
    public Result()
    {
        Errors = [];
    }

    public Result(Error error) : this()
    {
        Errors.Add(error);
    }

    public List<Error> Errors { get; set; }

    public bool HasErrors => Errors.Count > 0;

    public bool Succeeded => !HasErrors;

    public void AddError(Error error)
    {
        Errors.Add(error);
    }

    public static implicit operator Result(List<Error> errors) => new(errors);

    public static implicit operator Result(PreconditionError error) => new((Error)error);

    public static implicit operator Result(InfrastructureError error) => new((Error)error);
}
