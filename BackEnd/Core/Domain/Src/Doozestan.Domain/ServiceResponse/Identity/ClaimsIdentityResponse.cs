using System.Runtime.Serialization;
using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceResponse.Identity
{
    [DataContract]
    public class ClaimsIdentityResponse : BaseResponse
    {
        [DataMember]
        public string UserId { get; set; }
    }
}
