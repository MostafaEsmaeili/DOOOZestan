using System;
using Castle.Facilities.AutoTx;
using Castle.Facilities.NHibernate;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Framework.DataAccess;
using Framework.DataAccess.DataContext;
using Framework.Entity;
using Framework.IO;
using Framework.Mapper;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Framework.IoC
{
    public class CoreContainer
    {
        public static IWindsorContainer Container { get; set; }

        //public static CustomDbContext Context { get; set; }
        public static BootstrapperSettings Settings { get; set; }
        public static bool Initialized;

        private static void PreventReInitialize()
        {
            if (Initialized) throw new Exception("Core is already initialized!");
        }

        public static void Initialize()
        {
            Initialize(null);
        }
        public static void Initialize(BootstrapperSettings settings)
        {
            Initialize(new WindsorContainer(), settings);
        }

        public static void Initialize(IWindsorContainer container, BootstrapperSettings settings)
        {
            if (settings == null) settings = new BootstrapperSettings();
            Settings = settings;

            PreventReInitialize();
            Initialized = true;
            Container = container;

            switch (Settings.ORMFrameWork)
            {
                case ORMFrameWork.EntityFrameWok:
                    {
                        Container
                                 .Install(new SubsystemInstaller())
                                 .Install(new DataAccessInstaller(), new ServicesInstaller())
                                 .Register(
                                       Component.For<Database>().ImplementedBy(settings.DataBaseImplementation).LifeStyle.Singleton,
                                       Component.For<DataContext>().ImplementedBy(settings.EntityFramWorkInstallerImplementation).LifeStyle.Singleton
                                 );
                    }
                    break;
                case ORMFrameWork.NHibernate:
                    {
                        Container
                                  .Install(new SubsystemInstaller())
                                  .Install(new DataAccessInstaller(), new ServicesInstaller())
                                  .AddFacility<AutoTxFacility>()
                                  .Register(
                                      Component.For<Database>().ImplementedBy(settings.DataBaseImplementation).LifeStyle.Singleton,
                                      Component.For<INHibernateInstaller>()
                                          .ImplementedBy(settings.NhibernateInstallerImplementation)
                                          .LifeStyle.Singleton
                                  )
                                  .AddFacility<NHibernateFacility>(x =>
                                  {
                                      x.FlushMode = settings.NHibernateFlushMode;
                                      x.DefaultLifeStyle = settings.NHibernateSessionLifeStyle;
                                      x.LifeStyleScoped = settings.NHibernateSessionLifeStyleScoped;
                                  });
                    }
                    break;
            }


            RowMapper.Init(new InitRowMapParam { MapFileDirectory = PathHelper.BinPath() });
        }
    }
}
