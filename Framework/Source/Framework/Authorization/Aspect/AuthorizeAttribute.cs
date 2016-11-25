using System;

namespace Framework.Authorization.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute
    {
        public string OperationId { get; private set; }

        public AuthorizeAttribute(string operationId)
        {
            OperationId = operationId;
        }
    }
}
