using System;

namespace Framework.Utility
{
    public static class TypeFinder
    {
        public static bool IsTypeDerivedFromGenericType(this Type typeToCheck, Type genericType)
        {
            if (typeToCheck == typeof(object))
            {
                return false;
            }
            if (typeToCheck == null)
            {
                return false;
            }
            if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
            return IsTypeDerivedFromGenericType(typeToCheck.BaseType, genericType);
        }
    }
}
