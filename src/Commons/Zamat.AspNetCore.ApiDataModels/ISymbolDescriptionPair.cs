namespace AUMS.AspNetCore.ApiDataModels;

/// <summary>
/// Represents a business key-value dictionary  entry containing
/// information about the unique <see cref="Symbol"/> of a given
/// resource and its <see cref="Description"/> displayed in the UI.
/// </summary>
public interface ISymbolDescriptionPair
{
    /// <summary>
    /// Unique key that identifies a given resource in the system.
    /// </summary>
    string Symbol { get; }

    /// <summary>
    /// Description of the resource that will be visible in the user interface.
    /// </summary>
    string Description { get; }
}
