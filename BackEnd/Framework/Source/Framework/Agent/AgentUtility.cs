using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Agent
{
    public class AgentUtility
    {
        public static IEnumerable<Type> GetDerivedClass(Type type)
        { 
            try
            {
                return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(t => t.IsSubclassOf(type));
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
