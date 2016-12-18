using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public interface IServiceRepositoryService : IService<ServiceRepository>
    {
        List<ServiceRepository> GetServiceRepositories();

        ServiceRepository GetServiceRepository(string srviceName, string methodName);
    }
}
