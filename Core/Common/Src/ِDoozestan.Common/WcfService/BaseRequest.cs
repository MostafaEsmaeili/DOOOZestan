using System.Runtime.Serialization;

namespace Doozestan.Common.WcfService
{
    [DataContract]
    public class BaseRequest
    {

        [DataMember]
        public string AuthenticationToken { get; set; }

        [DataMember]
        public string ServiceUser { get; set; }

    }
}
