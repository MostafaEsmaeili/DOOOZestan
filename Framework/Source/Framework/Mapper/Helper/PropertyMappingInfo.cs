using System.Reflection;

namespace Framework.Mapper.Helper
{
    public sealed class PropertyMappingInfo
    {
        #region Private Variables

        private string _dataFieldName;
        private object _defaultValue;
        private PropertyInfo _propInfo;
        private bool _defaultValueSet;
        #endregion

        #region Constructors

        internal PropertyMappingInfo()
            : this(string.Empty, null, null,false){}

        internal PropertyMappingInfo(string dataFieldName, object defaultValue, PropertyInfo info,bool defaultValueSet)
        {
            _dataFieldName = dataFieldName;
            _defaultValue = defaultValue;
            _propInfo = info;
            _defaultValueSet = defaultValueSet;
        }

        #endregion

        #region Public Properties

        public string DataFieldName
        {
            get 
            {
                if (string.IsNullOrEmpty(_dataFieldName))
                {
                    _dataFieldName = _propInfo.Name;
                }
                return _dataFieldName;
            }
        }

        public object DefaultValue
        {
            get { return _defaultValue; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propInfo; }
        }

        public bool DefaultValueSet
        {
            get
            {
                return _defaultValueSet;
            }
        }

        internal void SetDataFieldName(string dataFieldName)
        {
            this._dataFieldName = dataFieldName;
        }
        #endregion
    }
}
