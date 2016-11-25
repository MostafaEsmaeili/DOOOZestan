using Castle.Windsor;
using Castle.Windsor.Configuration;

namespace Doozestan.Common
{
    public class PostponableContainer : WindsorContainer
    {
        public PostponableContainer(IConfigurationInterpreter interpreter)
            : base(interpreter)
        {
        }

        public PostponableContainer()
        {
        }

        protected override void RunInstaller()
        {
        }

        /// <summary>
        /// Call Run when registration finished
        /// </summary>
        public void Run()
        {
            base.RunInstaller();
        }
    }
}