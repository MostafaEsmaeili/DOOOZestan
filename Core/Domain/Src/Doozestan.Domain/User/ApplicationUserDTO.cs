using System;
using System.Runtime.Serialization;

namespace Doozestan.Domain.User
{

    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsActive { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }

        [IgnoreDataMember]
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
    }
}
