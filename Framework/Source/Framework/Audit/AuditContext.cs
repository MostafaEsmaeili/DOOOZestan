using System.Collections.Generic;
using System.Security.Principal;

namespace Framework.Audit
{
    public class Audit
    {
        public IPrincipal Principal { get; set; }
        public IEnumerable<AuditItem> AuditItem { get; set; }
    } 

    public class AuditItem
    {
        public string ObjectId { get; set; }
        public string OperationId { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
    }
}