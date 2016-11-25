using Castle.MicroKernel.Registration;
using Framework.Entity;

namespace Framework.Extensions
{
    public static class IoCExtensions
    {
        public static BasedOnDescriptor ApplyLifeStyle(this BasedOnDescriptor lifeStyle, BootstrapperSettings settings)
        {
            if (settings.Platform == Platform.Web)
                return lifeStyle.LifestylePerWebRequest();

            return lifeStyle.LifestyleSingleton();
        }
    }
}
