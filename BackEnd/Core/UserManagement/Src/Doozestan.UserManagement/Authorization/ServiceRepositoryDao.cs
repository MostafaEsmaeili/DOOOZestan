using System;
using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceRepositoryDao : AbstractDao<ServiceRepository>, IServiceRepositoryDao
    {
        private CustomLogger Logger => new CustomLogger(GetType().FullName);
        //public StagingDatabase StagingDatabase { get; set; }

        public ServiceRepositoryRowMapper ServiceRepositoryRowMapper = new ServiceRepositoryRowMapper();
        public List<ServiceRepository> GetServiceRepositories()
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("sec.GetServiceRepositories");
                return Entlib.ExecuteCommandAccessor(command, ServiceRepositoryRowMapper).ToList();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public ServiceRepository GetServiceRepository(string serviceName, string methodName)
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("sec.GetServiceRepository");
                return Entlib.ExecuteCommandAccessor(command, ServiceRepositoryRowMapper).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }
}
