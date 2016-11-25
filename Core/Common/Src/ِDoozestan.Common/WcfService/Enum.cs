using System.Runtime.Serialization;

namespace Doozestan.Common.WcfService
{
    [DataContract]
    public enum ResponseStatus
    {
        [EnumMember]
        Ok,
        [EnumMember]
        ExpectationFailed,
        [EnumMember]
        BadRequest,
        [EnumMember]
        MethodNotAllowed
    }

}
