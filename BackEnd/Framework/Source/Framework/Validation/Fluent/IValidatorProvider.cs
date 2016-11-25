using System;
using FluentValidation.Results;

namespace Framework.Validation.Fluent
{
    public interface IValidatorProvider
    {
        ValidationResult GetValidationForModel(Type type, object instance);
    }
}
