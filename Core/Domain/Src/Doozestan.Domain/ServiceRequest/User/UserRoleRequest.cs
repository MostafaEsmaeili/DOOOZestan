using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceRequest.User
{
    [DataContract]
    public class UserRoleRequest: BaseRequest
    {
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public Role Role { get; set; }
    }
}
