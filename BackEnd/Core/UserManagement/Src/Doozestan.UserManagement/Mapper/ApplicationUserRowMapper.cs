using System.Data;
using Doozestan.Domain;
using Framework.Mapper;
using Framework.Utility;

namespace Doozestan.UserManagement.Mapper
{
    public class ApplicationUserRowMapper : BaseMapper<ApplicationUser>
    {
        public override ApplicationUser InnerMapRow(IDataRecord reader)
        {
            ColumnPrefix = string.Empty;
            var entity = new ApplicationUser
            {
                Id = reader.SafeReader(ColumnPrefix + "Id").SafeString(),
                Email = reader.SafeReader(ColumnPrefix + "Email").SafeString(),
                EmailConfirmed = reader.SafeReader(ColumnPrefix + "EmailConfirmed").SafeBool(),
                PasswordHash = reader.SafeReader(ColumnPrefix + "PasswordHash").SafeString(),
                SecurityStamp = reader.SafeReader(ColumnPrefix + "SecurityStamp").SafeString(),
                PhoneNumber = reader.SafeReader(ColumnPrefix + "PhoneNumber").SafeString(),
                PhoneNumberConfirmed = reader.SafeReader(ColumnPrefix + "PhoneNumberConfirmed").SafeBool(),
                TwoFactorEnabled = reader.SafeReader(ColumnPrefix + "TwoFactorEnabled").SafeBool(),
                LockoutEndDateUtc = reader.SafeReader(ColumnPrefix + "LockoutEndDateUtc").SafeDateTime(),
                LockoutEnabled = reader.SafeReader(ColumnPrefix + "LockoutEnabled").SafeBool(),
                AccessFailedCount = reader.SafeReader(ColumnPrefix + "AccessFailedCount").SafeInt(),
                UserName = reader.SafeReader(ColumnPrefix + "UserName").SafeString(),
                DisplayName = reader.SafeReader(ColumnPrefix + "DisplayName").SafeString(),
                CreateDate = reader.SafeReader(ColumnPrefix + "CreateDate").SafeDateTime(),
                IsAdmin = reader.SafeReader(ColumnPrefix + "IsAdmin").SafeBool(),
                Status = reader.SafeReader(ColumnPrefix + "Status").SafeInt(),
            };
            return entity;
        }
    }
}