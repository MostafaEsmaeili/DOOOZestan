using System;

namespace Doozestan.Common.BaseException
{
    public class DoozestanBaseException : Exception
    {
        public DoozestanBaseException()
            : base()
        { }

        public DoozestanBaseException(string message)
            : base(message)
        { }

        public DoozestanBaseException(string format, params object[] args)
            : base(string.Format(format, args))
        { }

        public DoozestanBaseException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DoozestanBaseException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        { }
    }
}
