using System;
using System.Runtime.Serialization;

namespace Framework.Utility
{
    [DataContract]
    public enum ConditionType
    {
        [EnumMember]
        Or = 1,
        [EnumMember]
        And = 2
    }

    //[DataContract]
    //public enum FirmType
    //{
    //    LLCO = 1,
    //    OPCO,
    //    COCO,
    //    PRJC,
    //    PUJC,
    //    COMP,
    //    FOBR,
    //    JSPA,
    //    LSPA,
    //    INST,
    //    PLCO
    //}



    [DataContract]
    public enum SearchCondition
    {
        [EnumMember]
        Equal = 1,
        [EnumMember]
        Like = 2,
        [EnumMember]
        LikeFirst = 3,
        [EnumMember]
        LikeLast = 4,
        [EnumMember]
        In = 5,
        [EnumMember]
        IsNull = 6,
        [EnumMember]
        Greater = 7,
        [EnumMember]
        GreaterOrEqual = 8,
        [EnumMember]
        Less = 9,
        [EnumMember]
        LessOrEqual = 10
    }

    [DataContract]
    public enum SortDirection
    {
        [EnumMember]
        Asc,

        [EnumMember]
        Desc
    }

    [DataContract, Flags]
    public enum SearchValueType
    {
        [EnumMember]
        Number = 1,
        [EnumMember]
        String = 2,
        [EnumMember]
        DateTime = 4


    }

    public enum AutoCompleteType
    {
        Local = 1,
        Ajax = 2
    }

    [DataContract]
    public enum AggregateFunction
    {
        [EnumMember]
        None = 1,
        [EnumMember]
        AVG = 2,
        [EnumMember]
        COUNT = 3,
        [EnumMember]
        MAX = 4,
        [EnumMember]
        MIN = 5,
        [EnumMember]
        SUM = 6
    }

}
