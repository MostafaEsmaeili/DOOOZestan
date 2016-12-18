using System.Data;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceAccessRowMapper : BaseMapper<Domain.Entity.Staging.ServiceAccess>
    {
        public override Domain.Entity.Staging.ServiceAccess InnerMapRow(IDataRecord reader)
        {
            ColumnPrefix = string.Empty;
            var entity = new Domain.Entity.Staging.ServiceAccess
            {
                Id = reader.SafeReader(ColumnPrefix + "Id").SafeInt(),
                Allow = reader.SafeReader(ColumnPrefix + "Allow").SafeBool(),
                LastUpdate = reader.SafeReader(ColumnPrefix + "LastUpdate").SafeDateTime(),
                RoleId = reader.SafeReader(ColumnPrefix + "RoleId").SafeString(),
                UserId = reader.SafeReader(ColumnPrefix + "UserId").SafeString(),
                ServiceId = reader.SafeReader(ColumnPrefix + "ServiceId").SafeInt(),
                //ApplicationId = reader.SafeReader(ColumnPrefix + "ApplicationId").SafeInt()

            };
            return entity;
        }
    }
}
