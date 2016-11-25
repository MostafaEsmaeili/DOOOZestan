
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Discriminator { get; set; }

        public virtual System.Collections.Generic.ICollection<AspNetUserRole> AspNetUserRoles { get; set; }

        public AspNetRole()
        {
            AspNetUserRoles = new System.Collections.Generic.List<AspNetUserRole>();
        }
    }

}

