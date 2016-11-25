
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
        public string IdentityUserId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }

}

