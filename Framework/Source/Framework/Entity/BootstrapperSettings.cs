using System;
using Castle.Facilities.NHibernate;
using Framework.DataAccess.Entlib;
using Framework.NHibernate;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NHibernate;

namespace Framework.Entity
{
    public class BootstrapperSettings
    {
        public BootstrapperSettings(Type context, ORMFrameWork ormFrameWork = ORMFrameWork.NHibernate, Platform platform = Platform.Web)
        {
            Platform = platform;
            ORMFrameWork = ormFrameWork;
            switch (ormFrameWork)
            {
                case ORMFrameWork.EntityFrameWok:
                    {
                        EntityFramWorkInstallerImplementation = context;
                    }
                    break;
                case ORMFrameWork.NHibernate:
                    {
                        NhibernateInstallerImplementation = typeof(NHibInstaller);
                        NHibernateFlushMode = FlushMode.Auto;
                    }
                    break;
            }
            DataBaseImplementation = typeof(CustomSqlDatabase);
           
        }
        public BootstrapperSettings(Platform platform = Platform.Web)
        {
            Platform = platform;
            NhibernateInstallerImplementation = typeof(NHibInstaller);
            DataBaseImplementation = typeof(CustomSqlDatabase);
            NHibernateFlushMode = FlushMode.Auto;
        }

        private DefaultSessionLifeStyleOption? _NHibernateSessionLifeStyle;

        public FlushMode NHibernateFlushMode { get; set; }
        public Platform Platform { get; private set; }

        public ORMFrameWork ORMFrameWork { get; private set; }


        public Type DataBaseImplementation { get; private set; }

        public Type NhibernateInstallerImplementation { get; private set; }

        public Type EntityFramWorkInstallerImplementation { get; private set; }

        public Type NHibernateSessionLifeStyleScoped { get; private set; }

        public void DataBaseImplementedBy<T>() where T : Database
        {
            DataBaseImplementation = typeof(T);
        }

        public void NHibernateInstallerImplementedBy<T>() where T : INHibernateInstaller
        {
            NhibernateInstallerImplementation = typeof(T);
        }

        public void NHibernateSessionLifeStyleScopedBy<T>()
        {
            NHibernateSessionLifeStyleScoped = typeof(T);
        }

        public DefaultSessionLifeStyleOption NHibernateSessionLifeStyle
        {
            get
            {
                if (!_NHibernateSessionLifeStyle.HasValue)
                    switch (Platform)
                    {
                        case Platform.Web:
                            return DefaultSessionLifeStyleOption.SessionPerWebRequest;
                        default:
                            return DefaultSessionLifeStyleOption.SessionTransient;
                    }

                return _NHibernateSessionLifeStyle.Value;
            }
            set
            {
                _NHibernateSessionLifeStyle = value;
            }
        }
    }
}