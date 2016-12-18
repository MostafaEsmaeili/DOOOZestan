using System;
using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceRepositoryService : Service<ServiceRepository, IServiceRepositoryDao>, IServiceRepositoryService
    {
        private CustomLogger Logger => new CustomLogger(GetType().FullName);
        public List<ServiceRepository> GetServiceRepositories()
        {
            try
            {
                return Dao.GetServiceRepositories();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public ServiceRepository GetServiceRepository(string srviceName, string methodName)
        {
            try
            {
                return Dao.GetServiceRepository(srviceName, methodName);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }
}
