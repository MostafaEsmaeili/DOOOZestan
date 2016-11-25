using System.Runtime.Serialization;
using Doozestan.Common.WcfService;
using Doozestan.Domain;
using Doozestan.Domain.User;

namespace Doozestan.UserManagement.Login
{
    [DataContract]
    public class UserLoginResponse : BaseResponse
    {
        public ApplicationUserDTO ApplicationUser { get; set; }

        public LoginStatusEnum LoginStatusEnum { get; set; }
    }
}
