using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Framework.Mapper.Helper
{
    internal static class PropertyMapCache
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> propertyCache = new ConcurrentDictionary<Type, PropertyInfo[]>();
        public static PropertyInfo[] GetProperties(Type type)
        {
            if (propertyCache.ContainsKey(type))
                return propertyCache[type];
            PropertyInfo[] propertyInfos = type.GetProperties();
            propertyCache.AddOrUpdate(type, propertyInfos, (k, o) => propertyInfos);
            return propertyInfos;
        }

        public static PropertyInfo GetProperty(Type type, string propertName)
        {
            return GetProperties(type).FirstOrDefault(x => x.Name.Equals(propertName,StringComparison.InvariantCultureIgnoreCase));

        }
    }
}
