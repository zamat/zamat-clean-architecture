namespace Zamat.Common.Crud;

public abstract record Error(Enum ErrorCode, string ErrorMessage);
