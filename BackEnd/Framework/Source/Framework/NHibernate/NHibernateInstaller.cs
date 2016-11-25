using System;
using System.Collections.Generic;
using System.Data;
using Castle.Facilities.NHibernate;
using Castle.Transactions;
using Framework.IoC;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Framework.NHibernate
{
    public class NHibInstaller : INHibernateInstaller
    {
        protected Configuration Configuration { get; set; }

        public bool IsDefault
        {
            get { return true; }
        }

        public string SessionFactoryKey
        {
            get { return "default"; }
        }

        public Maybe<IInterceptor> Interceptor
        {
            get { return Maybe.None<IInterceptor>(); }
        }

        public Configuration Config
        {
            get
            {
                if (Configuration == null)
                {
                    Configuration = new Configuration();
                    Configuration.DataBaseIntegration(db =>
                    {
                        db.Dialect<MsSql2012Dialect>();
                        db.Driver<SqlClientDriver>();
                        db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                        db.IsolationLevel = IsolationLevel.ReadCommitted;
                        db.ConnectionProvider<ConnectionProviderWrapper>();
                        db.ConnectionString = "NothingImportant";
                        db.Timeout = 10;
                        db.BatchSize = 20;
                        db.ConnectionReleaseMode = ConnectionReleaseMode.AfterTransaction;

                        //enabled for testing
                        db.LogFormattedSql = true;
                        db.LogSqlInConsole = true;
                        db.AutoCommentSql = true;
                    });

                    //prevents NH to connect to database on startup
                    Configuration.SetProperty(global::NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "none");
                    Configuration.SetProperty(global::NHibernate.Cfg.Environment.CommandTimeout, "30");

                    AddMappings();
                }

                return Configuration;
            }
        }

        protected virtual void AddMappings()
        {
            var mapper = new ModelMapper();
            var types = new List<Type>();

            var mappings = CoreContainer.Container.ResolveAll(typeof(IEntitySqlsMapper));

            foreach (var map in mappings)
            {
                types.Add(map.GetType());
                types.Add(map.GetType().BaseType.GetGenericArguments()[0]);
            }

            mapper.AddMappings(types);
            Configuration.AddDeserializedMapping(mapper.CompileMappingFor(types), null);
        }

        public void Registered(ISessionFactory factory)
        {
        }
    }
}