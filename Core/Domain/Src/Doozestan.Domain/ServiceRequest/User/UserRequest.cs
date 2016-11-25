using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceRequest.User
{
    [DataContract]
    public class UserRequest : BaseRequest
    {
        [DataMember]
        public string UserId { get; set; }
    }
}
