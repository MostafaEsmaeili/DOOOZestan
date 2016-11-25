using FluentValidation;
using Framework.Validation.Fluent;

namespace Framework.Validation.Service
{
    public class ServiceValidatorProvider : AbstractValidatorProvider
    {
        public override IValidatorFactory ValidatorFactory { get; set; }
    }
}
