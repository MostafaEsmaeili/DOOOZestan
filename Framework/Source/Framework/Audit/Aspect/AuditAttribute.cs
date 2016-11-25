using System;

namespace Framework.Audit.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuditAttribute : Attribute
    {
        public string OperationId { get; private set; }
        public string DomainCode { get; private set; }
        public string Description { get; set; }
        public string ObjectId { get; set; }

        public AuditAttribute(string operationId, string domainCode)
        {
            OperationId = operationId;
            DomainCode = domainCode;
        }
    }
}
