using System.Runtime.Serialization;
using Doozestan.Common.WcfService;
using Doozestan.Domain.User;

namespace Doozestan.Domain.ServiceResponse.Login
{
    [DataContract]
    public class UserLoginResponse : BaseResponse
    {
        [DataMember]
        public ApplicationUserDTO ApplicationUser { get; set; }
        
        [DataMember]
        public LoginStatusEnum LoginStatusEnum { get; set; }
    }
}
