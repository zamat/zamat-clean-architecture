namespace Zamat.Common.Crud;

public record InfrastructureError(Enum ErrorCode, string ErrorMessage) : Error(ErrorCode, ErrorMessage);
