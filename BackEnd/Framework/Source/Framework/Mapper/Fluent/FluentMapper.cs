using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Mapper.Fluent
{
    public interface IMapHelper
    {
        void Map(string columnName);
        void UseMap(Type mapType);
        void UseMap<TMapType>() where TMapType : IMapper;
    }
    public interface IMapper
    {
        RowMap GetMap();
    }
    public abstract class FluentMapper<T> : IMapper
    {
        
        private class MapClass : IMapHelper
        {
            public enum MapType
            {
                UseMap,
                ColumnName
            }
            public MapClass(MemberExpression memberExpression)
            {
                _memberAccess = memberExpression;
            }
            public void Map(string columnName)
            {
                _columnName = columnName;
                _mapType = MapType.ColumnName;
            }
            public void UseMap(Type mapType)
            {
                _mapTypeName = mapType;
            }

            public void UseMap<TMapType>() where TMapType : IMapper
            {
                UseMap(typeof(TMapType));
            }

            private MapType _mapType;
            private string _columnName;
            private Type _mapTypeName;
            private string _propertyName;
            private MemberExpression _memberAccess;
            public ColumnMap GetMap()
            {
                
                MemberExpression temp = _memberAccess;
                string propertyMap = "";
                while (temp != null)
                {
                    propertyMap = "." + temp.Member.Name + propertyMap;
                    temp = temp.Expression as MemberExpression;
                }
                propertyMap = propertyMap.TrimStart('.');
                _propertyName = propertyMap;
                if (_mapType == MapType.UseMap && propertyMap.Contains("."))
                    throw new Exception("Nested Property And UseMap Is Not Compatible");
                ColumnMap columnMap=null;
                if (_mapType == MapType.ColumnName)
                {
                    string columnName = _columnName;
                    if (propertyMap.Contains("."))
                    {
                        string[] strings = propertyMap.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries);


                        RowMap tempRm = RowMapper.GetTempRowMap(strings, columnName);
                        columnMap = new ColumnMap()
                            {
                                PropertyName = strings[0],
                                InnerType = InnerMapType.Internal,
                                InternalRowMap = tempRm
                            };
                    }
                    else
                        columnMap = new ColumnMap() {ColumnName = columnName, PropertyName = propertyMap};
                }
                else if (_mapType == MapType.UseMap)
                {
                    columnMap = new ColumnMap()
                    {
                        PropertyName = propertyMap,
                        InnerType = InnerMapType.Internal,
                
                    };
                }
                return columnMap;
            }
            public string GetPropName()
            {
                return _propertyName;
            }
        }
        private List<IMapHelper> maps = new List<IMapHelper>();
        public Type MapType
        {
            get { return typeof(T); }
        }
        protected IMapHelper MapFor<TValue>(Expression<Func<T, TValue>> expression)
        {

            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new Exception("Expressin Must Be A MemberExpression");
            MapClass mapClass = new MapClass(memberExpression);
            maps.Add(mapClass);
            return mapClass;
        }

        private RowMap _rowMap;
        public RowMap GetMap()
        {
            if (_rowMap == null)
            {
                _rowMap = new RowMap();
                foreach (MapClass mapClass in maps)
                {
                    ColumnMap mapFunc = mapClass.GetMap();
                    if (_rowMap.ColumnMapsByPropertyName==null)
                        _rowMap.ColumnMapsByPropertyName=new Dictionary<string, ColumnMap>();
                    _rowMap.ColumnMapsByPropertyName.Add(mapClass.GetPropName(), mapFunc);
                }
            }
            return _rowMap;
        }
    }
}
