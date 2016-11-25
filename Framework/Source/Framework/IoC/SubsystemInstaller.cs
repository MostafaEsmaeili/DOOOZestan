using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Framework.IO;

namespace Framework.IoC
{
    public class SubsystemInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.InternalService.dll")));
            container.Install(FromAssembly.InDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.ExternalServices.dll")));
            //container.Install(FromAssembly.InDirectory(new AssemblyFilter(PathHelper.BinPath(), "*.Repository.dll")));
        }
    }
}