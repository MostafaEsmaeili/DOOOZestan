using System.Collections.Generic;

namespace Framework.ErrorHandler.Exceptions
{
    public class ValidatorException : AppException
    {
        private List<PropertyValidatorError> _propertList = new List<PropertyValidatorError>();
        public List<PropertyValidatorError> PropertList
        {
            get { return _propertList; }
            set { _propertList = value; }
        }

        public ValidatorException()
            : base("Validator Cant validate the input, for more information Use PropertyList", (int)BaseErrorCodes.ValidatorException)
        {

        }

        public ValidatorException(List<PropertyValidatorError> propertyList)
            : this()
        {

            _propertList = propertyList;
        }
    }
}
