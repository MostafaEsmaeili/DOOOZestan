using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceResponse.User
{
    [DataContract]
    public class UserRoleResponse : BaseResponse
    {
        [DataMember]
        public string UserId { get; set; }
    }
}
