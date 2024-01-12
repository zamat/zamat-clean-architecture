namespace Zamat.BuildingBlocks.Domain;

public class DomainResultError
{
    public DomainResultError(Enum errorCode, string error)
    {
        ErrorCode = errorCode;
        Error = error;
    }

    public Enum ErrorCode { get; }

    public string Error { get; }

    public override string ToString() => Error;
}
