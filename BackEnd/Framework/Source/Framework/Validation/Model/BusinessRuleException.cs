using System;
using System.Collections.Generic;
using Framework.ErrorHandler.Exceptions;

namespace Framework.Validation.Model
{
    public class BusinessRuleException : AppException
    {
        /// <summary>
        /// The exception code 
        /// </summary>
        public RuleExceptionCode Code { get; set; }

        /// <summary>
        /// The parameters included with exception
        /// </summary>
        public IEnumerable<object> Parameters { get; set; }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        public BusinessRuleException(string memberName, string errorMessage)
            : base(errorMessage, -1)
        {
            MemberName = memberName;
            ErrorMessage = errorMessage;
        }

        public BusinessRuleException(string memberName, string errorMessage, Exception innerException)
            : base(errorMessage, innerException, -1)
        {
            MemberName = memberName;
            ErrorMessage = errorMessage;
        }
    }
}
