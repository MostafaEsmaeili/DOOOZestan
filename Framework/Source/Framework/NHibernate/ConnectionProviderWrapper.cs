using Framework.IoC;
using NHibernate.Connection;
using IConnectionProvider = Framework.Connection.IConnectionProvider;

namespace Framework.NHibernate
{
    public class ConnectionProviderWrapper : DriverConnectionProvider
    {
        public IConnectionProvider DefaultConnectionProvider
        {
            get
            {
                return CoreContainer.Container.Resolve<IConnectionProvider>();
            }
        }

        public override System.Data.IDbConnection GetConnection()
        {
            return DefaultConnectionProvider.GetConnection(true);
        }
    }
}
