using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public interface IServiceAccessService : IService<ServiceAccess>
    {
        List<ServiceAccess> GetServiceAccesses();
        bool IsAccess(string roleId, string userId, int serviceId);
        void UpdateAccess(ServiceAccess access);
        void SaveAccess(ServiceAccess access);
        ApplicationUser GetApplicationUserById(string userId);
    }
}
