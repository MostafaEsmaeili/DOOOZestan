using Doozestan.Common.WcfService;
using Doozestan.Domain.User;

namespace Doozestan.Domain.ServiceRequest.User
{
    public class ApplicationUserRequest : BaseRequest
    {
        public ApplicationUserDTO ApplicationUser { get; set; }
    }
}
