using Doozestan.Common.WcfService;

namespace Doozestan.Domain.ServiceRequest.User
{
    public class ChangePasswordRequest : BaseRequest
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
