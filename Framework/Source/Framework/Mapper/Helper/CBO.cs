using System;
using System.Collections.Generic;
using System.Data;

namespace Framework.Mapper.Helper
{
    #region

    

    #endregion

    public static class CBO
    {
        #region Public Methods

        /// <summary>
        ///     Creates and populates a collection with instances of the objType object passed in.
        /// </summary>
        /// <typeparam name="T"> Type of object to add to the collection. </typeparam>
        /// <typeparam name="C"> </typeparam>
        /// <param name="objType"> Type of object to add to the collection. </param>
        /// <param name="dr"> IDataReader that contains the data for the collection. </param>
        public static C FillCollection<T, C>(Type objType, IDataReader dr)
            where T : class, new()
            where C : ICollection<T>, new()
        {
            C coll = new C();
            List<PropertyMappingInfo> mapInfo = GetProperties(objType);
            int[] ordinals = GetOrdinals(mapInfo, dr);

            while (dr.Read())
            {
                T obj = CreateObject<T>(dr, mapInfo, ordinals);
                coll.Add(obj);
            }
            return coll;
        }

        /// <summary>
        ///     Creates and populates an instance of the objType Type.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <param name="objType"> Type of the object to instantiate. </param>
        /// <param name="dr"> IDataReader with the data to populate the object instance with. </param>
        /// <returns> An instance of the objType type. </returns>
        public static T FillObject<T>(Type objType, IDataReader dr) where T : class, new()
        {
            T obj;

            List<PropertyMappingInfo> mapInfo = GetProperties(objType);
            int[] ordinals = GetOrdinals(mapInfo, dr);
            obj = CreateObject<T>(dr, mapInfo, ordinals);
            return obj;
        }

        /// <summary>
        ///     Creates and populates an instance of the objType Type.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <param name="objType"> Type of the object to instantiate. </param>
        /// <param name="dr"> IDataReader with the data to populate the object instance with. </param>
        /// <param name="mapInfo"> </param>
        /// <returns> An instance of the objType type. </returns>
        public static T FillObject<T>(Type objType, IDataReader dr, List<PropertyMappingInfo> mapInfo)
            where T : class, new()
        {
            T obj = null;

            try
            {
                int[] ordinals = GetOrdinals(mapInfo, dr);
                obj = CreateObject<T>(dr, mapInfo, ordinals);
            }
            catch
            {
                //TODO:Logger
               
            }

            return obj;
        }

        public static bool HasColumn(string columnName, IDataReader dr, DataTable retrivedColumn)
        {
            columnName = columnName.ToLower();
            foreach (DataRow row in retrivedColumn.Rows)
            {
                if (row["ColumnName"] != null && row["ColumnName"].ToString().ToLower() == columnName
                    && dr[columnName] != DBNull.Value)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasColumn(string columnName, DataTable retrivedColumn)
        {
            columnName = columnName.ToLower();
            foreach (DataRow row in retrivedColumn.Rows)
            {
                if (row["ColumnName"] != null && row["ColumnName"].ToString().ToLower() == columnName)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Methods

        internal static PropertyMappingInfo GetPropertyMappingInfo(List<PropertyMappingInfo> lst, string dataFieldName)
        {
            foreach (PropertyMappingInfo propertyMappingInfo in lst)
            {
                if (propertyMappingInfo.DataFieldName == dataFieldName)
                {
                    return propertyMappingInfo;
                }
            }
            return null;
        }

        /// <summary>
        ///     Loads the PropertyMappingInfo collection for the type specified by objType from the cache, or creates the collection and adds it to the cache if it does not exist.
        /// </summary>
        /// <param name="objType"> Type to load the properties for. </param>
        /// <returns> A collection of PropertyMappingInfo objects that are associated with the Type. </returns>
        internal static List<PropertyMappingInfo> LoadPropertyMappingInfo(Type objType)
        {
            
            List<PropertyMappingInfo> mapInfoList = new List<PropertyMappingInfo>();
            //foreach (PropertyInfo info in objType.GetProperties())
            //{
            //    Attribute[] attribs = Attribute.GetCustomAttributes(info, typeof(DataMappingAttribute), false);
            //    if (attribs != null)
            //    {
            //        foreach (Attribute item in attribs)
            //        {
            //            DataMappingAttribute mapAttr = item as DataMappingAttribute;

            //            if (mapAttr != null)
            //            {
            //                PropertyMappingInfo mapInfo = new PropertyMappingInfo(
            //                    mapAttr.DataFieldName, mapAttr.NullValue, info, mapAttr.IsDefaultSet);
            //                mapInfoList.Add(mapInfo);
            //            }
            //        }
            //    }
            //}

            return mapInfoList;
        }

        /// <summary>
        ///     Iterates through the object type's properties and attempts to assign the value from the datareader to the matching property.
        /// </summary>
        /// <typeparam name="T"> The type of object to populate. </typeparam>
        /// <param name="dr"> The IDataReader that contains the data to populate the object with. </param>
        /// <param name="propInfoList"> List of PropertyMappingInfo objects. </param>
        /// <param name="ordinals"> Array of integers that indicate the index into the IDataReader to get the value from. </param>
        /// <returns> A populated instance of type T </returns>
        private static T CreateObject<T>(IDataReader dr, List<PropertyMappingInfo> propInfoList, int[] ordinals)
            where T : class, new()
        {
            T obj = new T();

            for (int i = 0; i <= propInfoList.Count - 1; i++)
            {
                if (propInfoList[i].PropertyInfo.CanWrite)
                {
                    Type type = propInfoList[i].PropertyInfo.PropertyType;
                    object value = propInfoList[i].DefaultValue;

                    if (ordinals[i] != -1 && dr.IsDBNull(ordinals[i]) == false)
                    {
                        value = dr.GetValue(ordinals[i]);

                        try
                        {
                            if (type.BaseType != null && type.BaseType.Equals(typeof(Enum)))
                            {
                                if (value is string)
                                {
                                    propInfoList[i].PropertyInfo.SetValue(obj, Enum.Parse(type, value.ToString()), null);
                                }
                                else
                                {
                                    propInfoList[i].PropertyInfo.SetValue(obj, Enum.ToObject(type, value), null);
                                }
                            }
                            else
                            {
                                propInfoList[i].PropertyInfo.SetValue(obj, value, null);
                            }
                        }
                        catch
                        {
                            try
                            {
                                if (type.BaseType.Equals(typeof(Enum)))
                                {
                                    if (value is string)
                                    {
                                        propInfoList[i].PropertyInfo.SetValue(
                                            obj, Enum.Parse(type, value.ToString()), null);
                                    }
                                    else
                                    {
                                        propInfoList[i].PropertyInfo.SetValue(obj, Enum.ToObject(type, value), null);
                                    }
                                }

                                else if (type.BaseType.Equals(typeof(Object)) || 
                                    (
                                    
                                    type.BaseType.BaseType.Equals(typeof(Object)) && 
                                    !type.BaseType.Equals(typeof(ValueType))
                                    
                                    ))
                                {
                                  //  BaseEventLogs.Write(string.Format("Type Can't Be Cast {0}",type.ToString()));
                                }
                                else
                                {
                                    // try explicit conversion
                                    propInfoList[i].PropertyInfo.SetValue(obj, Convert.ChangeType(value, type), null);
                                }
                            }
                            catch
                            {
                                //TODO:Logger
                               
                            }
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        ///     Returns an array of integers that correspond to the index of the matching field in the PropertyInfoCollection.
        /// </summary>
        /// <param name="propMapList"> PropertyMappingInfo Collection </param>
        /// <param name="dr"> DataReader </param>
        /// <returns> Array of integers that correspond to the field's index position in the datareader for each one of the PropertyMappingInfo objects. </returns>
        private static int[] GetOrdinals(List<PropertyMappingInfo> propMapList, IDataReader dr)
        {
            int[] ordinals = new int[propMapList.Count];

            if (dr != null)
            {
                DataTable dt = dr.GetSchemaTable();

                Dictionary<string, string> Columnlst = new Dictionary<string, string>();
                foreach (DataRow row in dt.Rows)
                {
                    string CN = row["ColumnName"].ToString().ToLower();
                    Columnlst.Add(CN, CN);
                }
                for (int i = 0; i <= propMapList.Count - 1; i++)
                {
                    ordinals[i] = -1;
                    try
                    {
                        string cName = propMapList[i].DataFieldName.ToLower();
                        if (Columnlst.ContainsKey(cName))
                        {
                            ordinals[i] = dr.GetOrdinal(cName);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //TODO:Logger
                      

                    }
                    catch
                    {
                        //TODO:Logger
                       

                    }
                }
            }

            return ordinals;
        }

        /// <summary>
        ///     Loads the PropertyMappingInfo collection for type specified.
        /// </summary>
        /// <param name="objType"> Type that contains the properties to load. </param>
        /// <returns> A collection of PropertyMappingInfo objects that are associated with the Type. </returns>
        private static List<PropertyMappingInfo> GetProperties(Type objType)
        {
            List<PropertyMappingInfo> info = MappingInfoCache.GetCache(objType.Name);

            if (info == null)
            {
                info = LoadPropertyMappingInfo(objType);
                MappingInfoCache.SetCache(objType.Name, info);
            }
            return info;
        }

        #endregion
    }
}
