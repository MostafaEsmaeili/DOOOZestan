using Doozestan.Common.WcfService;

namespace Doozestan.UserManagement.User
{
    public class ApplicationUserResponse : BaseResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
