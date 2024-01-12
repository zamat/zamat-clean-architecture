using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace AUMS.AspNetCore.ApiDataModels.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the display name of an enumeration value.
    /// </summary>
    /// <param name="enumValue">The enumeration value.</param>
    /// <returns>The display name of the enumeration value, or <see langword="null"/> if no display name is found.</returns>
    public static string? GetDisplayName(this Enum enumValue)
    {
        var displayAttribute = enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DisplayAttribute>();

        if (displayAttribute is null)
        {
            return null;
        }

        return displayAttribute.GetName();
    }

    /// <summary>
    /// Retrieves the display description of an enumeration value.
    /// </summary>
    /// <param name="enumValue">The enumeration value.</param>
    /// <returns>The display description of the enumeration value, or <see langword="null"/> if no display description is found.</returns>
    public static string? GetDisplayDescription(this Enum enumValue)
    {
        var displayAttribute = enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DisplayAttribute>();

        if (displayAttribute is null)
        {
            return null;
        }

        return displayAttribute.GetDescription();
    }

    /// <summary>
    /// Converts an enumeration value to an <see cref="ISymbolDescriptionPair"/>.
    /// </summary>
    /// <param name="enumValue">The enumeration value.</param>
    /// <returns>An <see cref="ISymbolDescriptionPair"/> that represents the enumeration value.</returns>
    /// <exception cref="DataModelException">Thrown when the display name of the enumeration value cannot be determined.</exception>
    public static ISymbolDescriptionPair ToSymbolDescriptionPair(this Enum enumValue)
    {
        var displayName =
            enumValue.GetDisplayDescription()
            ?? throw new DataModelException(
                $"Failed to determine the correct Description for element {nameof(enumValue)} while converting to {nameof(ISymbolDescriptionPair)}."
            );

        return new SymbolDescriptionPair(enumValue.ToString(), displayName);
    }

    /// <summary>
    /// Converts an enumeration value to an <see cref="ISymbolDescriptionPair"/> using a specified string localizer.
    /// </summary>
    /// <param name="enumValue">The enumeration value.</param>
    /// <param name="stringLocalizer">The string localizer to use when converting the enumeration value.</param>
    /// <returns>An <see cref="ISymbolDescriptionPair"/> that represents the enumeration value.</returns>
    /// <exception cref="DataModelException">Thrown when the display name of the enumeration value cannot be determined.</exception>
    public static ISymbolDescriptionPair ToSymbolDescriptionPair(
        this Enum enumValue,
        IStringLocalizer stringLocalizer
    )
    {
        var displayName =
            enumValue.GetDisplayDescription()
            ?? throw new DataModelException(
                $"Failed to determine the correct Description for element {nameof(enumValue)} while converting to {nameof(ISymbolDescriptionPair)}."
            );

        return new SymbolDescriptionPair(enumValue.ToString(), stringLocalizer[displayName]);
    }
}
