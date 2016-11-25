using System;
using Doozestan.Common.WcfService;

namespace Doozestan.UserManagement.User
{
    public class ApplicationUserRequest : BaseRequest
    { 
        public string DisplayName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }
        public string Password { get; set; }
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
    }
}
