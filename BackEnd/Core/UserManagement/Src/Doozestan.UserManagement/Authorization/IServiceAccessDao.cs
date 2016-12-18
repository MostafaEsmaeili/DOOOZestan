using System.Collections.Generic;
using Doozestan.Domain;
using Framework.DataAccess.Repositories;

namespace Doozestan.UserManagement.Authorization
{
    public interface IServiceAccessDao : IDao<Domain.Entity.Staging.ServiceAccess>
    {
        List<Domain.Entity.Staging.ServiceAccess> GetServiceAccesses();
        bool IsAccess(string roleId, string userId, int serviceId);
        void UpdateAccess(ServiceAccess access);
        void SaveAccess(ServiceAccess access);
        ApplicationUser GetApplicationUserById(string userId);
    }
}
