using System.Data;

namespace Doozestan.UserManagement.Authorization
{
    public class ServiceRepositoryRowMapper : BaseMapper<Domain.Entity.Staging.ServiceRepository>
    {
        public override ServiceRepository InnerMapRow(IDataRecord reader)
        {
            ColumnPrefix = string.Empty;
            var entity = new ServiceRepository
            {
                Id = reader.SafeReader(ColumnPrefix + "Id").SafeInt(),
                Code = reader.SafeReader(ColumnPrefix + "Code").SafeString(),
                MethodName = reader.SafeReader(ColumnPrefix + "MethodName").SafeString(),
                ServiceName = reader.SafeReader(ColumnPrefix + "ServiceName").SafeString(),
                Title = reader.SafeReader(ColumnPrefix + "Title").SafeString()
            };
            return entity;
        }
    }
}
