using System;
using System.Collections.Generic;
using AUMS.AspNetCore.ApiDataModels.Extensions;
using Microsoft.Extensions.Localization;

namespace AUMS.AspNetCore.ApiDataModels.Converters;

/// <summary>
/// Provides methods to convert enumeration values to symbol-description pairs.
/// </summary>
public static class EnumToSymbolConverter
{
    /// <summary>
    /// Converts all values of a specified enumeration to a collection of symbol-description pairs.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <returns>A collection of symbol-description pairs representing the enumeration values.</returns>
    public static IEnumerable<ISymbolDescriptionPair> Convert<TEnum>()
        where TEnum : Enum
    {
        var symbols = new List<ISymbolDescriptionPair>();
        var values = (TEnum[])Enum.GetValues(typeof(TEnum));

        foreach (var value in values)
        {
            symbols.Add(value.ToSymbolDescriptionPair());
        }

        return symbols;
    }

    /// <summary>
    /// Converts all values of a specified enumeration to a collection of symbol-description pairs, using a specified string localizer.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <param name="stringLocalizer">The string localizer to use when converting the enumeration values.</param>
    /// <returns>A collection of symbol-description pairs representing the enumeration values.</returns>
    public static IEnumerable<ISymbolDescriptionPair> Convert<TEnum>(
        IStringLocalizer stringLocalizer
    )
        where TEnum : Enum
    {
        var symbols = new List<ISymbolDescriptionPair>();
        var values = (TEnum[])Enum.GetValues(typeof(TEnum));

        foreach (var value in values)
        {
            symbols.Add(value.ToSymbolDescriptionPair(stringLocalizer));
        }

        return symbols;
    }
}
