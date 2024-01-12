using System;

namespace AUMS.AspNetCore.ApiDataModels;

/// <summary>
/// Represents errors that occur during application execution related to data models.
/// </summary>
public class DataModelException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataModelException"/> class.
    /// </summary>
    public DataModelException()
        : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataModelException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DataModelException(string message)
        : base(message) { }
}
