using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;

namespace IdentityServer3.Admin.EntityFramework.CustomService
{
   public class ClientCustomService :ClientStore
    {
       public ClientCustomService(IClientConfigurationDbContext context) : base(context)
       {
       }
    }
    public class  ScopeCustomService : ScopeStore
    {
        public ScopeCustomService(IScopeConfigurationDbContext context) : base(context)
        {
           
        }
    }
    public class  AuthStor:AuthorizationCodeStore
    {
        public AuthStor(IOperationalDbContext context, IScopeStore scopeStore, IClientStore clientStore) : base(context, scopeStore, clientStore)
        {
        }
    }
}
