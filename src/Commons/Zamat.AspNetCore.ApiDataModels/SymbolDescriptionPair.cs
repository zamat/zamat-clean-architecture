using System;
using System.Collections.Generic;
using System.Linq;

namespace AUMS.AspNetCore.ApiDataModels;

/// <summary>
/// Represents a business key-value dictionary  entry containing
/// information about the unique <see cref="Symbol"/> of a given
/// resource and its <see cref="Description"/> displayed in the UI.
/// </summary>
public class SymbolDescriptionPair : ISymbolDescriptionPair
{
    public SymbolDescriptionPair(string symbol, string description)
    {
        Symbol = symbol;
        Description = description;
    }

    private SymbolDescriptionPair() { }

    /// <inheritdoc />
    public virtual string Symbol { get; }

    /// <inheritdoc />
    public virtual string Description { get; }

    public static bool operator ==(SymbolDescriptionPair a, SymbolDescriptionPair b) =>
        a.Equals((object)b);

    public static bool operator !=(SymbolDescriptionPair a, SymbolDescriptionPair b) => !(a == b);

    /// <summary>
    /// Determines whether the specified object components are equal to the current object components.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><see langword="true"/> if the specified object components are equal to the current object components; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not SymbolDescriptionPair other)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) => (current * 23) + (obj?.GetHashCode() ?? 0));
    }

    /// <summary>
    /// Gets the components that determine the equality of the current object.
    /// </summary>
    /// <returns>An enumeration of the components that are used for equality checks.</returns>
    protected virtual IEnumerable<object> GetEqualityComponents()
    {
        yield return Symbol;
        yield return Description;
    }
}
