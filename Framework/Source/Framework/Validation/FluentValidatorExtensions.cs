using FluentValidation;
using Framework.ErrorHandler.Exceptions;

namespace Framework.Validation
{
    public static class FluentValidatorExtensions
    {
        public static FluentValidation.IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this FluentValidation.IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder).WithState(x => PropertyValidatorReason.Empty);
        }
        public static FluentValidation.IRuleBuilderOptions<T, TProperty> SetState<T, TProperty>(this FluentValidation.IRuleBuilderOptions<T, TProperty> rule, PropertyValidatorReason reason)
        {
            return rule.WithState(x => reason);
        }
    }
}