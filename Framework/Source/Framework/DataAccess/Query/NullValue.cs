using System;
using System.Runtime.Serialization;

namespace Framework.DataAccess.Query
{
    [DataContract(Name = "NullVal")]
    public enum NullValue
    {
        [EnumMember]
        IsNull = 0,

        [EnumMember]
        IsNotNull = 1
    }
}