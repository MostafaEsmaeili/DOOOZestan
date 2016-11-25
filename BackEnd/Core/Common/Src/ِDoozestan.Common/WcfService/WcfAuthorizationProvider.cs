using Framework.Authorization;

namespace Doozestan.Common.WcfService
{
    public class WcfAuthorizationProvider : IAuthorizationProvider<ServiceAuthorizationContext>
    {
        public bool CheckAccess(ServiceAuthorizationContext context)
        {
            if (context.UserName == "Tandis")
                return true;
            else
            {
                return false;
            }
        }
    }
}
