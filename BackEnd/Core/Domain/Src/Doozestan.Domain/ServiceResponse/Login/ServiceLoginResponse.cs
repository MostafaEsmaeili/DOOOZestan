using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceResponse.Login
{
    [DataContract]
    public class ServiceLoginResponse : BaseResponse
    {
        [DataMember]
        public string AuthenticationToken { get; set; }

        [DataMember]
        public LoginStatusEnum LoginStatusEnum { get; set; }
    }
}
