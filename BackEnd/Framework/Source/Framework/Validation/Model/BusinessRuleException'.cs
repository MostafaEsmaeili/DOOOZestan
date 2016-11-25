using System;

namespace Framework.Validation.Model
{
    public class BusinessRuleException<T> : BusinessRuleException
    {
        public BusinessRuleException(string memberName, string errorMessage)
            : base(memberName, errorMessage)
        {
        }

        public BusinessRuleException(string memberName, string errorMessage, Exception innerException)
            : base(memberName, errorMessage, innerException)
        {
        }

        public T ExceptionObject { get; set; }
    }
}
