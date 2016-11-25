using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Framework.IO;
using Framework.Service;
using Repository.Pattern;

namespace Framework.IoC
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            FromAssemblyDescriptor fromAssemblyInDirectory = Classes.FromAssemblyInDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.InternalService.dll"));
            container.Register(fromAssemblyInDirectory.BasedOn(typeof(IService<>)).WithService.DefaultInterfaces());
        }
    }
}