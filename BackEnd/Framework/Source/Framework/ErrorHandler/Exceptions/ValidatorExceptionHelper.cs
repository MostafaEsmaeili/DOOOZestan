using System.Collections.Generic;

namespace Framework.ErrorHandler.Exceptions
{
    public class ValidatorExceptionHelper
    {
        public static void ThrowException(List<PropertyValidatorError> propertyList)
        {
            throw new ValidatorException(propertyList);
        }
    }
}