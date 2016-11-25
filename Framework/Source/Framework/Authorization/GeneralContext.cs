using System.Collections.Generic;
using System.Security.Principal;

namespace Framework.Authorization
{
    public class GeneralContext
    {
        public IPrincipal Principal { get; set; }
        public IEnumerable<string> OperationIdList { get; set; }
    }
}