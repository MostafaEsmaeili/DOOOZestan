using System;
using System.Configuration;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;

namespace Framework.Authorization
{
    public class NetSqlAuthorizationProvider : IAuthorizationProvider<ServiceAuthorizationContext>
    {
        //public ILog Logger
        //{
        //    get { return LogConfigurator.GetCurrentLogger(); }
        //}

        protected string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["NetSqlAuthorizationConnectionString"].ConnectionString; }
        }
        protected string ApplicationName
        {
            get { return ConfigurationManager.AppSettings["NetSqlApplicationName"]; }
        }
        protected string StoreName
        {
            get { return ConfigurationManager.AppSettings["NetSqlStoreName"]; }
        }

        private IAzManStorage _storage;
        protected IAzManStorage Storage
        {
            get { return _storage ?? (_storage = new SqlAzManStorage(ConnectionString)); }
        }

        private IAzManApplication _application;
        protected IAzManApplication Application
        {
            get { return _application ?? (_application = Storage.Stores[StoreName].Applications[ApplicationName]); }
        }

        public bool CheckAccess(ServiceAuthorizationContext context)
        {
            var methodName = context.RequestUri.AbsolutePath;
            var auth = AuthorizationType.Deny;

            try
            {
                var dbUser = Storage.GetDBUser(context.UserName);

                try
                {
                    auth = Storage.CheckAccess(StoreName, ApplicationName, methodName, dbUser, DateTime.Now, true);
                }
                catch (Exception e)
                {
                    //Logger.Error(string.Format("Service authorization not found. user : {0} method : {1} method : {2}", context.UserName, methodName, methodName), e);
                }
            }
            catch (Exception e)
            {
                //Logger.Error(string.Format("Service authorization dbUser not found for user : {0}", context.UserName), e);
            }

            return auth == AuthorizationType.Allow || auth == AuthorizationType.AllowWithDelegation;
        }
    }
}
