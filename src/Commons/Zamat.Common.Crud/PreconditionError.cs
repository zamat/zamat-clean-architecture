namespace Zamat.Common.Crud;

public record PreconditionError(Enum ErrorCode, string ErrorMessage) : Error(ErrorCode, ErrorMessage);
