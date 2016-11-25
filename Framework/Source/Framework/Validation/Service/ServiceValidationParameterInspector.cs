using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using FluentValidation.Results;
using Framework.Validation.Fluent;
using Framework.Validation.Model;
using ValidationFailure = Framework.Validation.Model.ValidationFailure;

namespace Framework.Validation.Service
{
    public class ServiceValidationParameterInspector : IParameterInspector
    {
        public IValidatorProvider ValidatorProvider { get; set; }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            foreach (var input in inputs)
                if (input != null)
                {
                    var result = ValidatorProvider.GetValidationForModel(input.GetType(), input);
                    if (result != null && !result.IsValid)
                        ThrowException(result);
                }

            return null;
        }

        private void ThrowException(ValidationResult result)
        {
            throw new FaultException<ValidationResultContract>(ConvertValidationResultToValidationResultFaultException(result)
                                                               , "Validation violation!", new FaultCode("Validation"));
        }

        protected virtual ValidationResultContract ConvertValidationResultToValidationResultFaultException(ValidationResult result)
        {
            return new ValidationResultContract
            {
                Errors = result.Errors.Select(x => new ValidationFailure
                {
                    ErrorMessage = x.ErrorMessage,
                    MemberName = x.PropertyName
                }).ToList()
            };
        }
    }
}
