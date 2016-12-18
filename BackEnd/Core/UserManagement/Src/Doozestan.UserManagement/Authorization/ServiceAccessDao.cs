using System;
using System.Collections.Generic;
using System.Data;
using Doozestan.UserManagement.Mapper;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceAccessDao : AbstractDao<ServiceAccess>, IServiceAccessDao
    {
        private CustomLogger Logger => new CustomLogger(GetType().FullName);
        //public Entlib Entlib { get; set; }

        public ServiceAccessRowMapper ServiceAccessRowMapper { get; set; }
        public ApplicationUserRowMapper ApplicationUserRowMapper = new ApplicationUserRowMapper();
        public List<ServiceAccess> GetServiceAccesses()
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("sec.GetServiceAccesses");
                return Entlib.ExecuteCommandAccessor(command, ServiceAccessRowMapper).ToList();
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        public bool IsAccess(string roleId, string userId, int serviceId)
        {
            IDataReader reader = null;
            var res = false;
            try
            {
                var command = Entlib.GetStoredProcCommand("sec.IsAccess");
                Entlib.AddInParameter(command, "roleid", DbType.String, roleId);
                Entlib.AddInParameter(command, "userid", DbType.String, userId);
                Entlib.AddInParameter(command, "serviceid", DbType.Int32, serviceId);
                reader = Entlib.ExecuteReader(command);
                if (reader.Read())
                    res = reader[0].SafeBoolean();
                return res;
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        public void SaveAccess(ServiceAccess access)
        {
            try
            {
                var command = Entlib.GetStoredProcCommand("sec.SaveAccess");
                Entlib.AddInParameter(command, "RoleId", DbType.String, access.RoleId);
                Entlib.AddInParameter(command, "userid", DbType.String, access.UserId);
                Entlib.AddInParameter(command, "allow", DbType.Boolean, access.Allow);
                //Entlib.AddInParameter(command, "ApplicationId", DbType.Int32, access.ApplicationId);
                Entlib.AddInParameter(command, "ServiceId", DbType.Int32, access.ServiceId);
                Entlib.AddInParameter(command, "LastUpdate", DbType.DateTime, access.LastUpdate);
                Entlib.ExecuteNonQuery(command);
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
                var command = Entlib.GetStoredProcCommand("sec.GetApplicationUserById");
                Entlib.AddInParameter(command, "UserId", DbType.String, userId);
                var res = Entlib.ExecuteCommandAccessor(command, ApplicationUserRowMapper);
                return res != null ? res.ToList().FirstOrDefault() : new ApplicationUser();
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
                var command = Entlib.GetStoredProcCommand("sec.SaveAccess");
                Entlib.AddInParameter(command, "RoleId", DbType.String, access.RoleId);
                Entlib.AddInParameter(command, "userid", DbType.String, access.UserId);
                Entlib.AddInParameter(command, "allow", DbType.Boolean, access.Allow);
                //Entlib.AddInParameter(command, "ApplicationId", DbType.Int32, access.ApplicationId);
                Entlib.AddInParameter(command, "ServiceId", DbType.Int32, access.ServiceId);
                Entlib.AddInParameter(command, "LastUpdate", DbType.DateTime, access.LastUpdate);
                Entlib.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }
    }
}
