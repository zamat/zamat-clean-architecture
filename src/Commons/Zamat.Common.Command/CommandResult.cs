using System;
using System.Collections.Generic;

namespace Zamat.Common.Command;

public record CommandResult
{
    public List<CommandError> Errors { get; set; }
    public bool HasErrors => Errors.Count > 0;
    public bool Succeeded => !HasErrors;

    public void AddError(Enum errorCode, string errorMessage) 
        => Errors.Add(new CommandError(errorCode, errorMessage));

    public CommandResult()
    {
        Errors = new List<CommandError>();
    }

    public CommandResult(CommandError commandError) : this()
    {
        Errors.Add(commandError);
    }

    public static implicit operator CommandResult(List<CommandError> errors) => new(errors);
}

public record CommandError(Enum ErrorCode, string ErrorMessage);
