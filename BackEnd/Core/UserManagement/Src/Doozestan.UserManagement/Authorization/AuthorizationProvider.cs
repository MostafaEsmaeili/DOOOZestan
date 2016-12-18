using System;
using System.Linq;
using Doozestan.Domain;
using Framework.IoC;
using Framework.Logging;

namespace Doozestan.UserManagement.Authorization
{
    public class AuthorizationProvider
    {
        public IServiceAccessService ServiceAccessService => CoreContainer.Container.Resolve<IServiceAccessService>();
        public IServiceRepositoryService ServiceRepositoryService => CoreContainer.Container.Resolve<IServiceRepositoryService>();

        private CustomLogger Logger => new CustomLogger(GetType().FullName);
        public bool IsAccessWebRequest(ApplicationUser applicationUser, string serviceMethodName, string controller)
        {
            //try
            //{
            //    if (applicationUser.IsCustomizedAccess)
            //    {
            //        var repository = ServiceRepositoryService.GetServiceRepository(controller.Trim(), serviceMethodName.Trim());
            //        return repository != null && ServiceAccessService.IsAccess(null, applicationUser.Id, repository.Id);
            //    }
            //    else
            //    {
            //        var repository = ServiceRepositoryService.GetServiceRepository(controller.Trim(), serviceMethodName.Trim());
            //        var role = applicationUser.Roles.FirstOrDefault();
            //        return repository != null && ServiceAccessService.IsAccess(role.RoleId, applicationUser.Id, repository.Id);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.ErrorException(ex.Message, ex);
            //    throw ex;
            //}
            return false;
        }

        //public ApplicationUser GetApplicationUserById(string userId)
        //{
        //    try
        //    {
        //        return ServiceAccessService.GetApplicationUserById(userId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ErrorException(ex.Message, ex);
        //        throw ex;
        //    }
        //}

    }
}
