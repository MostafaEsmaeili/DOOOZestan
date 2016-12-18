using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public interface IServiceRepositoryDao : IDao<ServiceRepository>
    {
        List<ServiceRepository> GetServiceRepositories();
        ServiceRepository GetServiceRepository(string serviceName, string methodName);
    }
}
