namespace Zamat.Common.Command;

public record PreconditionError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);
