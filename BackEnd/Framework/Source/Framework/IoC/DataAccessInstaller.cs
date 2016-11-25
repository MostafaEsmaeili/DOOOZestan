using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.DataAccess.Repositories;
using Framework.IO;
using Framework.Mapper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Framework.IoC
{
    public class DataAccessInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            FromAssemblyDescriptor fromAssemblyInDirectory = Classes.FromAssemblyInDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.Dao.dll"));
            container.Register(fromAssemblyInDirectory.BasedOn(typeof(IDao<>)).WithService.AllInterfaces());
            //container.Register(fromAssemblyInDirectory.BasedOn(typeof(ClassMapping<>)).WithService.Select(new[] { typeof(IEntitySqlsMapper) }));
            //Base Mappers should not be Singletone because of renaming ColumnPrefix
            container.Register(fromAssemblyInDirectory.BasedOn(typeof(BaseMapper<>)).WithService.AllInterfaces().WithServiceBase().WithServiceSelf().LifestyleTransient());
            container.Register(fromAssemblyInDirectory.BasedOn(typeof(IResultSetMapper<>)).WithService.DefaultInterfaces());
        }
    }
}
