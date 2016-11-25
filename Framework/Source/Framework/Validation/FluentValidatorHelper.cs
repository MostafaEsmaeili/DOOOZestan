using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Framework.ErrorHandler.Exceptions;

namespace Framework.Validation
{
    public static class FluentValidatorHelper
    {
        private class GeneralValidator<T> : AbstractValidator<T>
        {
            public GeneralValidator(Func<T, bool>[] validationRules)
            {
                if (validationRules != null && validationRules.Length > 0)
                {
                    foreach (Func<T, bool> validationRule in validationRules)
                    {
                        RuleFor(x => x).Must(validationRule);
                    }
                    return;
                }
                throw new AppException("Validation Rules Must Has At leat One Memeber", (int)BaseErrorCodes.ValidatorException);
            }
        }

        public static void Validate<T>(T instance, Func<T, bool>[] validationRules)
        {
            var validator = new GeneralValidator<T>(validationRules);
            Validate(validator, instance);
        }
        public static void Validate<T>(AbstractValidator<T> validator, T instance, string ruleset = "")
        {
            ValidationResult validationResult;

            if (!string.IsNullOrEmpty(ruleset))
                validationResult = validator.Validate(instance, ruleSet: ruleset);
            else
                validationResult = validator.Validate(instance);
            HandleValidationResult(validationResult);
        }
        private static void HandleValidationResult(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                IList<ValidationFailure> validationFailures = validationResult.Errors;
                var invalidProperty = new List<PropertyValidatorError>();
                invalidProperty.AddRange(validationFailures.Select(x => new PropertyValidatorError(x.PropertyName, (x.CustomState == null) ? PropertyValidatorReason.InvalidData : (PropertyValidatorReason)x.CustomState, x.ErrorMessage)));
                if (invalidProperty.Count > 0)
                {
                    ValidatorExceptionHelper.ThrowException(invalidProperty);
                }
            }
        }
    }
}
