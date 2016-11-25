using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Strongshell.Recoil.Core.Composition;
using Strongshell.Recoil.Core.Integration;

namespace Framework.FluentApi
{
    public static class AssemblyScanner
    {
        public static void Scan(this IContainerSelector container, IEnumerable<Assembly> assemlies)
        {
            foreach (var type in assemlies.SelectMany(assembly => assembly.GetTypes().Where(x =>
                typeof(WiringContainer).IsAssignableFrom(x) && x != typeof(WiringContainer))))
                try
                {
                    container.With(type);
                }
                catch (Exception e)
                {
                    throw new Exception("AssemblyScanner failed.", e);
                }
        }
    }
}
