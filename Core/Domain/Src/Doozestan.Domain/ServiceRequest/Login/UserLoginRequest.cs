using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceRequest.Login
{

    [DataContract]
    public class UserLoginRequest : BaseRequest
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }


    }
}
