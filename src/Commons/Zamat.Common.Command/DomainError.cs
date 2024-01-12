namespace Zamat.Common.Command;

public record DomainError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);
