using FluentValidation;

namespace AUMS.Common.FluentValidation;

public static class IRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TElement> AlphanumericValidator<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new AlphanumericValidator<T, TElement>());
    }
}
