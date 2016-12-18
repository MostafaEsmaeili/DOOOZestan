using System;
using System.Collections.Generic;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceAccessService : Service<ServiceAccess, IServiceAccessDao>, IServiceAccessService
    {
        private CustomLogger Logger => new CustomLogger(GetType().FullName);
        public List<ServiceAccess> GetServiceAccesses()
        {
            try
            {
                return Dao.GetServiceAccesses();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public bool IsAccess(string roleId, string userId, int serviceId)
        {
            try
            {
                return Dao.IsAccess(roleId, userId, serviceId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public void UpdateAccess(ServiceAccess access)
        {
            try
            {
                Dao.UpdateAccess(access);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public void SaveAccess(ServiceAccess access)
        {
            try
            {
                Dao.SaveAccess(access);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public ApplicationUser GetApplicationUserById(string userId)
        {
            try
            {
                return Dao.GetApplicationUserById(userId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }
}
