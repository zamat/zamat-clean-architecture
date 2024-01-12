using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace AUMS.Common.FluentValidation;

public class AlphanumericValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "AlphanumericValidator";

    internal const string RegexExpression = @"^[a-zA-Z0-9_ ]+$";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is string stringValue)
        {
            var regex = new Regex(RegexExpression);
            return regex.IsMatch(stringValue);
        }

        return false;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "'{PropertyName}' contains invalid chars";
}
