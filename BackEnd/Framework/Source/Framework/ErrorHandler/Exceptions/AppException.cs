using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Framework.ErrorHandler.Exceptions
{
    public class AppException : Exception
    {
        public int ErrorCode { get; set; }
        public List<AppException> Errors { get; set; }
        public AppException(int errorCode = 0)
        {
            ErrorCode = errorCode;
        }

        public AppException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public AppException(string message)
            : base(message)
        {
        }

        public AppException(string message, Exception innerException, int errorCode)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected AppException(SerializationInfo info, StreamingContext context, int errorCode)
            : base(info, context)
        {
            ErrorCode = errorCode;
        }

    }
}
