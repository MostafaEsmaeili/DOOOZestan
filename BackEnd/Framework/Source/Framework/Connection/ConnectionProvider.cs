using System;
using System.Configuration;
using System.Data.Common;
using Framework.IoC;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Repository.Pattern;

namespace Framework.Connection
{
    public class AppConfingConnectionProvider : IConnectionProvider
    {
        private DbConnection Connection { get; set; }

        public AppConfingConnectionProvider(string connectionStringName)
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public string ConnectionString { get; set; }

        public Database Database
        {
            get
            {
                return CoreContainer.Container.Resolve<Database>();
            }
        }

        public DbConnection GetConnection(bool requiresOpen = false)
        {
            if (Connection == null)
            {
                Connection = Database.DbProviderFactory.CreateConnection();
                //BRule.Assert(Connection != null, "Connection != null",(int)BaseErrorCodes.UnknownError);
                Connection.ConnectionString = ConnectionString;
                Connection.Disposed += Connection_Disposed;
            }

            if (requiresOpen && Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();

            if (!requiresOpen && Connection.State != System.Data.ConnectionState.Closed)
                Connection.Close();

            return Connection;
        }

        void Connection_Disposed(object sender, EventArgs e)
        {
            Connection = null;
        }

        public void Dispose()
        {
            if (Connection != null)
                Connection.Dispose();
        }
    }
}