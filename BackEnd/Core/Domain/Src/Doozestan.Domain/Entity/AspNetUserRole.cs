
using Newtonsoft.Json;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string IdentityUserId { get; set; }

        [JsonIgnore]
        public virtual AspNetRole AspNetRole { get; set; }
        [JsonIgnore]
        public virtual AspNetUser AspNetUser { get; set; }
    }

}

