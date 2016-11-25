using System.Runtime.Serialization;
using Doozestan.Common.WcfService;
using Doozestan.Domain.User;

namespace Doozestan.Domain.ServiceResponse.User
{
    [DataContract]
    public class UserResponse : BaseResponse
    {
        [DataMember]
        public ApplicationUserDTO ApplicationUserDto { get; set; }
    }
}
