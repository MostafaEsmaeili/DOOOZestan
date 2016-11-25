using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Framework.Mapper.Helper;

namespace Framework.Mapper
{
    public class InitRowMapParam
    {
        public string MapFileDirectory { get; set; }
    }

    public interface IRowMapper
    {
        PopulateMethodDelegate<T> GetPopulateMethod<T>(string mapName) where T : new();
        T GetData<T>(string mapName, IDataReader dr) where T : new();
        string GetColumnName(string mapName, string propertyName);
    }

    public class RowMapper : IRowMapper
    {
        public static bool IsInited { get; private set; }
        private static object _lockObj = new object();
        private Dictionary<string, RowMap> _mappers;

        private string _mapFileDirectory;
        private static RowMapper _instance;

        public static RowMapper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RowMapper();
                return _instance;
            }
        }
        private RowMapper()
        {
            _mappers = new Dictionary<string, RowMap>();
        }
        public static RowMapper Init(InitRowMapParam initParam)
        {
            lock (_lockObj)
            {
                if (IsInited)
                    return _instance;
                _instance = new RowMapper();
                _instance._mapFileDirectory = !string.IsNullOrEmpty(initParam.MapFileDirectory) ? initParam.MapFileDirectory : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


                IsInited = true;
                return _instance;
            }

        }
        internal static RowMap GetTempRowMap(string[] s, string columnName, RowMap tempRm = null)
        {
            int length = s.Length;
            if (tempRm == null)
            {
                tempRm = new RowMap { ColumnMapsByPropertyName = new Dictionary<string, ColumnMap>() };
            }
            InnerMapType innerMapType = InnerMapType.Internal;
            RowMap internalRowMap = null;
            if (length > 2)
            {
                string[] strings = s.Skip(1).ToArray();
                RowMap rm = null;
                if (tempRm.ColumnMapsByPropertyName.ContainsKey(strings[0]))
                    rm = tempRm.ColumnMapsByPropertyName[strings[0]].InternalRowMap;
                internalRowMap = GetTempRowMap(strings, columnName, rm);
            }
            else
            {
                innerMapType = InnerMapType.None;
            }

            if (!tempRm.ColumnMapsByPropertyName.ContainsKey(s[1]))
                tempRm.ColumnMapsByPropertyName.Add(s[1], new ColumnMap { ColumnName = columnName, PropertyName = s[1], InnerType = innerMapType, InternalRowMap = internalRowMap });
            return tempRm;
        }
        public PopulateMethodDelegate<T> GetPopulateMethod<T>(string mapName) where T : new()
        {
            return (dr) => { return GetData<T>(mapName, dr); };
        }
        //public PopulateMethodDelegate<T> GetPopulateMethod<T>(Type mapName) where T : new()
        //{
        //    return (dr) => { return GetData<T>(mapName, dr); };
        //}
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
        private object GetData(string mapName, IDataReader dr, Type type, object defaultValue = null)
        {
            object rv;
            if (type.IsPrimitive)
            {
                rv = Convert.ChangeType(dr[0], type);
            }
            else
            {
                if (_mappers.ContainsKey(mapName))
                {
                    RowMap rowMap = _mappers[mapName];
                    rv = MapDataRow(dr, type, rowMap, mapName, defaultValue);
                }
                else
                {


                    throw new Exception("Map Not Found MapName=" + mapName);
                }
            }
            return rv;
        }
        private object GetData(Type mapType, IDataReader dr, Type type, object defaultValue = null)
        {
            object rv = null;
            if (type.IsPrimitive)
            {
                rv = Convert.ChangeType(dr[0], type);
            }
            else
            {
                //TODO: may be we dont need this


            }
            return rv;
        }
        private object MapDataRow(IDataReader dr, Type type, RowMap rowMap, string mapName, object defaultValue = null)
        {
            object obj;
            if (defaultValue != null)
            {
                obj = defaultValue;
            }
            else
            {
                obj = Activator.CreateInstance(type, null);
            }
            bool setReturnValue = false;
            object rv = null;
            DataTable schemaTable = dr.GetSchemaTable();
            foreach (KeyValuePair<string, ColumnMap> keyValuePair in rowMap.ColumnMapsByPropertyName)
            {
                switch (keyValuePair.Value.InnerType)
                {
                    case InnerMapType.None:
                        {
                            string columnName = keyValuePair.Value.ColumnName;
                            if (HasColumn(columnName, dr, schemaTable))
                            {
                                PropertyInfo propertyInfo = PropertyMapCache.GetProperty(type,
                                                                                         keyValuePair.Value.PropertyName);
                                if (propertyInfo != null)
                                {
                                    propertyInfo.SetValue(obj,
                                                          RowMapperChangeType(dr[columnName], propertyInfo.PropertyType),
                                                          null);
                                    setReturnValue = true;
                                }
                                else
                                {
                                    //TODO:Logger
                                    throw new Exception("PropertyName Not Found PropertyName=" +
                                                        keyValuePair.Value.PropertyName +
                                                        " MapName=" + mapName);
                                }
                            }
                        }
                        break;
                    case InnerMapType.Internal:
                    case InnerMapType.External:
                        {
                            PropertyInfo propertyInfo = PropertyMapCache.GetProperty(type,
                                                                                     keyValuePair.Value.PropertyName);
                            if (propertyInfo != null)
                            {
                                if (keyValuePair.Value.InnerType == InnerMapType.Internal)
                                {
                                    object value = propertyInfo.GetValue(obj, null);

                                    object data = MapDataRow(dr, propertyInfo.PropertyType, keyValuePair.Value.InternalRowMap, "Internal Map For " + mapName + " " + keyValuePair.Value.PropertyName, value);

                                    if (value == null)
                                        propertyInfo.SetValue(obj, data, null);
                                    setReturnValue = true;
                                }
                                else if (keyValuePair.Value.InnerType == InnerMapType.External)
                                {
                                    object data = GetData(keyValuePair.Value.InnerRowMapName, dr,
                                                          propertyInfo.PropertyType);
                                    propertyInfo.SetValue(obj, data, null);
                                    setReturnValue = true;
                                }
                            }
                            else
                            {
                                //TODO:Logger

                                throw new Exception("PropertyName Not Found PropertyName=" +
                                                           keyValuePair.Value.PropertyName +
                                                           " MapName=" + mapName);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }



            }
            if (setReturnValue)
                rv = obj;
            return rv;
        }

        private object RowMapperChangeType(object obj, Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
                type = underlyingType;
            if (type.IsEnum)
            {
                if (obj is string)
                    return Enum.Parse(type, obj.ToString());
                return Enum.ToObject(type, obj);
            }

            return Convert.ChangeType(obj, type);
        }

        public string GetColumnName(string mapName, string propertyName)
        {
            if (_mappers.ContainsKey(mapName))
            {
                string[] props = propertyName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                RowMap rowMap = _mappers[mapName];
                ColumnMap columnMap;
                int counter = 0;
                do
                {
                    string innerPropertyName = props[counter];
                    if (!rowMap.ColumnMapsByPropertyName.ContainsKey(innerPropertyName))
                        //throw new AppException("Row Mapper can not find property {PropertyName:" + propertyName +
                        //                       ",innerPropertyName:" + innerPropertyName + "}");
                        throw new Exception();
                    columnMap = rowMap.ColumnMapsByPropertyName[innerPropertyName];
                    switch (columnMap.InnerType)
                    {
                        case InnerMapType.None:
                            rowMap = null;
                            break;
                        case InnerMapType.External:
                            rowMap = _mappers[columnMap.InnerRowMapName];
                            break;
                        case InnerMapType.Internal:
                            rowMap = columnMap.InternalRowMap;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    counter++;
                } while (counter < props.Length && rowMap != null);
                if (columnMap != null)
                    return "[" + columnMap.ColumnName + "]";
            }
            throw new Exception("Map not found: " + mapName);
        }

        public T GetData<T>(string mapName, IDataReader dr) where T : new()
        {
            T obj = new T();
            return (T)GetData(mapName, dr, typeof(T), obj);
        }
        public T GetData<T>(Type mapperType, IDataReader dr) where T : new()
        {
            T obj = new T();
            return (T)GetData(mapperType, dr, typeof(T), obj);
        }
        public T GetData<T, TMapper>(IDataReader dr) where T : new()
        {
            return GetData<T>(typeof(TMapper), dr);
        }
    }
}
