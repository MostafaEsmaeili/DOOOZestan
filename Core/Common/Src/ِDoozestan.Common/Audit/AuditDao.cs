using System;
using System.Data;
using Framework.IoC;
using Framework.Logging;

namespace Doozestan.Common.Audit
{
    public class AuditDao
    {

        private CustomLogger Logger
        {
            get
            {
                return new CustomLogger(this.GetType().FullName);
            }
        }
        private CustomDatabase CustomDatabase
        {
            get
            {
                return CoreContainer.Container.Resolve<CustomDatabase>();
            }
        }

        public void Save(Auditing auditing)
        {
            try
            {
                var command = CustomDatabase.GetStoredProcCommand("dbo.SaveAuditing");
                CustomDatabase.AddInParameter(command, "UserName", DbType.String, auditing.UserName);
                CustomDatabase.AddInParameter(command, "UserId", DbType.String, auditing.UserId);
                CustomDatabase.AddInParameter(command, "IPAddress", DbType.String, auditing.IPAddress);
                CustomDatabase.AddInParameter(command, "UrlAccessed", DbType.String, auditing.UrlAccessed);
                CustomDatabase.AddInParameter(command, "TimeAccessed", DbType.DateTime, auditing.TimeAccessed);
                CustomDatabase.AddInParameter(command, "CurrentAction", DbType.String, auditing.CurrentAction);
                CustomDatabase.AddInParameter(command, "CurrentController", DbType.String, auditing.CurrentController);
                CustomDatabase.AddInParameter(command, "CurrentArea", DbType.String, auditing.CurrentArea);
                CustomDatabase.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
            }
        }
    }
}
