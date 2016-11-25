using System.Configuration;
using System.Data.Common;
using Framework.Connection;
using Framework.IoC;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Doozestan.Common
{
    public class DefaultDatabaseConnectionProvider : AppConfingConnectionProvider
    {
        public const string ConnectionStringName = "DoozestanDataContext";
        public DefaultDatabaseConnectionProvider()
            : base(ConfigurationManager.AppSettings[ConnectionStringName])
        {
        }
    }


    public class CustomDatabase : SqlDatabase
    {
        public IConnectionProvider ConnectionProvider => CoreContainer.Container.Resolve<IConnectionProvider>(DefaultDatabaseConnectionProvider.ConnectionStringName);

        public CustomDatabase()
            : base("DoozestanDataContext")
        {
        }

        public override DbConnection CreateConnection()
        {
            return ConnectionProvider.GetConnection();
        }
    }
}
