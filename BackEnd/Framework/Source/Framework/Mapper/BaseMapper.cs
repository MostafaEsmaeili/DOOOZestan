using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Framework.Mapper
{
    public abstract class BaseMapper<T> : IRowMapper<T>
    {
        private string _columnPrefix;
        public virtual string ColumnPrefix
        {
            get
            {
                if (_columnPrefix == null)
                    _columnPrefix = GetType().Name.Substring(0, GetType().Name.IndexOf("RowMapper", StringComparison.Ordinal));
                return _columnPrefix;
            }
            set { _columnPrefix = value; }
        }

        public T MapRow(IDataRecord reader)
        {
            //if (IsEntityAvailable(reader))
            return InnerMapRow(reader);
        }

        public abstract T InnerMapRow(IDataRecord reader);

        public virtual bool IsEntityAvailable(IDataRecord reader)
        {
            return true;
        }
    }
}