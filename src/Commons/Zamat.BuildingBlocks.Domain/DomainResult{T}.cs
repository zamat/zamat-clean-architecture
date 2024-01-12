using System.Collections.Immutable;

namespace Zamat.BuildingBlocks.Domain;

public sealed class DomainResult<T> : DomainResult
{
    public DomainResult()
        : base() { }

    public DomainResult(T result)
        : base()
    {
        Result = result;
    }

    public DomainResult(ImmutableArray<DomainResultError> errors)
        : base(errors) { }

    public DomainResult(params DomainResultError[] errors)
        : base(errors) { }

    public T Result { get; } = default!;

    public static implicit operator DomainResult<T>(T result) => new(result);

    public static implicit operator DomainResult<T>(DomainResultError error) => new(error);

    public static implicit operator DomainResult<T>(DomainResultError[] errors) => new(errors);

    public static implicit operator DomainResult<T>(ImmutableArray<DomainResultError> errors) => new(errors);

    public static implicit operator DomainResult<T>(HashSet<DomainResultError> errors) =>
        new(errors.ToImmutableArray());

    public static implicit operator DomainResult<T>(List<DomainResultError> errors) =>
        new(errors.ToImmutableArray());
}
