using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Doozestan.Common;
using Doozestan.Domain;
using Framework.Connection;
using Framework.DataAccess.Repositories;
using Framework.Entity;
using Framework.IoC;
using Framework.IO;
using Framework.Mapper;
using Framework.Service;
using NHibernate.Engine;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Doozestan.WebApi
{
    public class BootStrapper
    {

        public void Init()
        {
            InitIoC();
        }

        public void InitIoC()
        {
            CoreContainer.Initialize(new BootstrapperSettings(typeof(DoozestanDbContext), ORMFrameWork.EntityFrameWok, Platform.Win));

            CoreContainer.Container.Register(Component.For<IConnectionProvider>().ImplementedBy<DefaultDatabaseConnectionProvider>()
               .Named(DefaultDatabaseConnectionProvider.ConnectionStringName)
               .LifestylePerThread().IsDefault());


            //database
            CoreContainer.Container.Register(Component.For<CustomDatabase>().ImplementedBy<CustomDatabase>().LifeStyle.Singleton);

            //common service
            //FromAssemblyDescriptor fromAssemblyInDirectoryAggregator = Classes.FromAssemblyInDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.InternalService.dll"));
            ////container.Register(fromAssemblyInDirectoryAggregator.BasedOn(typeof(BaseAggregator<>)).WithService.DefaultInterfaces());
            //CoreContainer.Container.Install(FromAssembly.InDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.InternalServices.dll")));
            //DependencyResolver.SetResolver(new WindsorDependencyResolver(CoreContainer.Container));

            FromAssemblyDescriptor fromAssemblyInDirectory = Classes.FromAssemblyInDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.InternalService.dll"));
            CoreContainer.Container.Register(fromAssemblyInDirectory.BasedOn(typeof(IService<>)).WithService.AllInterfaces());
            CoreContainer.Container.Install(FromAssembly.InDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.Common.dll")));

            FromAssemblyDescriptor fromAssemblyInDirectory2 = Classes.FromAssemblyInDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.UserManagement.dll"));
            CoreContainer.Container.Register(fromAssemblyInDirectory2.BasedOn(typeof(IService<>)).WithService.DefaultInterfaces());
            CoreContainer.Container.Register(fromAssemblyInDirectory2.BasedOn(typeof(IDao<>)).WithService.AllInterfaces());
            CoreContainer.Container.Register(fromAssemblyInDirectory2.BasedOn(typeof(ClassMapping<>)).WithService.Select(new[] { typeof(IEntitySqlsMapper) }));
            //Base Mappers should not be Singletone because of renaming ColumnPrefix
            CoreContainer.Container.Register(fromAssemblyInDirectory2.BasedOn(typeof(BaseMapper<>)).WithService.AllInterfaces().WithServiceBase().WithServiceSelf().LifestyleTransient());


            //var assemblyDescriptor = Classes.FromThisAssembly();
            //CoreContainer.Container.Register(assemblyDescriptor.BasedOn<IController>().WithServiceSelf().LifestyleTransient());
            //ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(CoreContainer.Container.Kernel));


            //helpers
            CoreContainer.Container.Register(Component.For<TransactionHelper>().ImplementedBy<TransactionHelper>());
         
        }

    }
}