namespace Zamat.Common.Command;

public abstract record CommandError(Enum ErrorCode, string ErrorMessage);
