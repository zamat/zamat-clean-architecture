namespace Zamat.Common.Command;

public record InfrastructureError(Enum ErrorCode, string ErrorMessage) : CommandError(ErrorCode, ErrorMessage);
