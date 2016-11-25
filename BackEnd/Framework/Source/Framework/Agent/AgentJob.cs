using System;

namespace Framework.Agent
{
    public class AgentJob
    {
        AgentJob()
        {

        }

        public static int ExecuteAll(Type type)
        {
            var types = AgentUtility.GetDerivedClass(type);
        
            foreach (var t in types)
            {
                //QuartzJobAgent obj = (QuartzJobAgent)Activator.CreateInstance(t);
                //obj.ExecuteAgent();
                
            }
            return 0;
        }
    }
}
