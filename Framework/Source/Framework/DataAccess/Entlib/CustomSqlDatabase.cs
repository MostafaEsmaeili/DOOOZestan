using System.Data;
using System.Data.Common;
using Framework.Connection;
using Framework.IoC;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Repository.Pattern;

namespace Framework.DataAccess.Entlib
{
    public class CustomSqlDatabase : SqlDatabase
    {
        public IConnectionProvider DefaultConnectionProvider
        {
            get
            {
                return CoreContainer.Container.Resolve<IConnectionProvider>();
            }
        }

        public CustomSqlDatabase()
            : base("DoozestanDataContext")
        {
        }

        public override DbConnection CreateConnection()
        {
            return DefaultConnectionProvider.GetConnection();
        }

        /// <summary>
        ///		Gets a "wrapped" connection that will be not be disposed if a transaction is
        ///		active (created by creating a <see cref="TransactionScope"/> instance). The
        ///		connection will be disposed when no transaction is active.
        /// </summary>
        /// <returns></returns>
        protected override DatabaseConnectionWrapper GetOpenConnection()
        {
            DatabaseConnectionWrapper connection = TransactionScopeConnections.GetConnection(this);
            if (connection != null)
                connection.AddRef();
            return connection ?? GetWrappedConnection();
        }

        public override DatabaseConnectionWrapper GetWrappedConnection()
        {
            var con = base.GetWrappedConnection();
            con.AddRef();

            return con;
        }

        public override void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable,
            byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            if (dbType == DbType.String || dbType == DbType.StringFixedLength)
                value = value != null ? value.ToString() : null;

            base.AddParameter(command, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
        }
    }
}
