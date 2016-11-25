using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Validation.Model
{
    public class InputValidationException : Exception
    {
        public IEnumerable<ValidationFailure> Errors { get; private set; }

        public InputValidationException(IEnumerable<ValidationFailure> errors)
            : base(BuildErrorMesage(errors))
        {
            Errors = errors;
        }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            var arr = errors.Select(x => "\r\n -- " + x.ErrorMessage).ToArray();
            return "Validation failed: " + string.Join("", arr);
        }
    }
}
