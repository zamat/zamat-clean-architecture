namespace Zamat.Common.Command;

public sealed record CommandResult
{
    public CommandResult()
    {
        Errors = new List<CommandError>();
    }

    public CommandResult(CommandError commandError) : this()
    {
        Errors.Add(commandError);
    }

    public ICollection<CommandError> Errors { get; set; }

    public bool HasErrors => Errors.Count > 0;

    public bool Succeeded => !HasErrors;

    public bool IsDomainProblem => HasErrors && Errors.Any(e => e is DomainError);

    public object EventData { get; set; } = default!;

    public void AddError(CommandError error)
    {
        Errors.Add(error);
    }

    public static implicit operator CommandResult(PreconditionError error) => new((CommandError)error);

    public static implicit operator CommandResult(DomainError error) => new((CommandError)error);

    public static implicit operator CommandResult(InfrastructureError error) => new((CommandError)error);

    public static implicit operator CommandResult(List<CommandError> errors) => new(errors);
}
