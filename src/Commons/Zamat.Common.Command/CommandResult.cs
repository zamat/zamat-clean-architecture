namespace Zamat.Common.Command;

public sealed record CommandResult
{
    public List<CommandError> Errors { get; set; }
    public bool HasErrors => Errors.Count > 0;
    public bool Succeeded => !HasErrors;
    public bool IsDomainProblem => HasErrors && Errors.Any(e => e is DomainError);

    public void AddError(CommandError error) => Errors.Add(error);

    public CommandResult()
    {
        Errors = new List<CommandError>();
    }

    public CommandResult(CommandError commandError) : this()
    {
        Errors.Add(commandError);
    }

    public static implicit operator CommandResult(List<CommandError> errors) => new(errors);
}

public abstract record CommandError(Enum ErrorCode, string ErrorMessage);
public record PreconditionError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);
public record DomainError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);
public record InfrastructureError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);