using System.Collections.Immutable;

namespace Zamat.BuildingBlocks.Domain;

public class DomainResult
{
    public DomainResult()
    {
        Errors = Array.Empty<DomainResultError>().ToImmutableArray();
    }

    public DomainResult(ImmutableArray<DomainResultError> errors)
    {
        Errors = errors;
    }

    public DomainResult(params DomainResultError[] errors)
    {
        Errors = errors.ToImmutableArray();
    }

    public static DomainResult Empty => new();

    public ImmutableArray<DomainResultError> Errors { get; }

    public bool HasErrors => Errors.Length > 0;

    public bool Succeeded => !HasErrors;

    public static implicit operator DomainResult(DomainResultError error) => new(error);

    public static implicit operator DomainResult(DomainResultError[] errors) => new(errors);

    public static implicit operator DomainResult(ImmutableArray<DomainResultError> errors) => new(errors);

    public static implicit operator DomainResult(HashSet<DomainResultError> errors) =>
        new(errors.ToImmutableArray());

    public static implicit operator DomainResult(List<DomainResultError> errors) => new(errors.ToImmutableArray());

    public string GetErrorMessage() => HasErrors ? Errors.First().Error : string.Empty;
}
