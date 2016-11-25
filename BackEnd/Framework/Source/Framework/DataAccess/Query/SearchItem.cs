using System;
using System.Runtime.Serialization;

namespace Framework.DataAccess.Query
{
    public class SearchItem
    {
        public SearchItem()
        {
            ConditionType = ConditionType.And;
        }

        #region Fields

        private ConditionEquality _conditionEquality = ConditionEquality.Equality;
        private ConditionStringType _conditionStringType = ConditionStringType.In;

        #endregion Fields

        #region Properties

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public ConditionEquality ConditionEquality
        {
            get { return _conditionEquality; }
            set { _conditionEquality = value; }
        }

        [DataMember]
        public ConditionStringType ConditionStringType
        {
            get { return _conditionStringType; }
            set { _conditionStringType = value; }
        }

        [DataMember]
        public ConditionType ConditionType { get; set; }

        [DataMember]
        public SearchItem Item { get; set; }


        [DataMember]
        public object ValueInSearch { get; set; }

        #endregion Properties

    }
}