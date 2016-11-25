using System;
using FluentValidation;

namespace Framework.Validation.Fluent
{
    public abstract class AbstractValidatorProvider : IValidatorProvider
    {
        public virtual IValidatorFactory ValidatorFactory { get; set; }

        public virtual FluentValidation.Results.ValidationResult GetValidationForModel(Type type, object instance)
        {
            var validator = ValidatorFactory.GetValidator(type);
            if (validator == null)
                return null;

            return validator.Validate(instance);
        }
    }
}
