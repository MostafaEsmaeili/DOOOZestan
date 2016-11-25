using System.Collections.Generic;

namespace Framework.Mapper.Helper
{
    

    internal static class MappingInfoCache
    {
        private static Dictionary<string, List<PropertyMappingInfo>> cache = new Dictionary<string,List<PropertyMappingInfo>>();

        internal static List<PropertyMappingInfo> GetCache(string typeName)
        {
            List<PropertyMappingInfo> info = null;
            try
            {
                    if(cache.ContainsKey(typeName))
                        info = cache[typeName];
                
            }
            catch(KeyNotFoundException)
            {
                //TODO:Logger
          

            }

            return info;
        }

        internal static void SetCache(string typeName, List<PropertyMappingInfo> mappingInfoList)
        {
            cache[typeName] = mappingInfoList;
        }

        //public static void ClearCache()
        //{
        //    cache.Clear();
        //}
    }
    
}
