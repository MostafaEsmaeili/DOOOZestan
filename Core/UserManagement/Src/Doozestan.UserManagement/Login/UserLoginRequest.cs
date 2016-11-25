using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.UserManagement.Login
{

    [DataContract]
    public class UserLoginRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
