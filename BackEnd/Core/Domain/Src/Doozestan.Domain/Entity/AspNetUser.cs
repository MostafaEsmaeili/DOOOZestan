
using System;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public System.DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public System.DateTime? CreateDate { get; set; }
        public string Discriminator { get; set; }
        public bool? IsActive { get; set; }
        public  bool IsAdmin { get; set; }
        public  bool IsCustomizedAccess { get; set; }
        public  int? Status { get; set; }

        public virtual System.Collections.Generic.ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual System.Collections.Generic.ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual System.Collections.Generic.ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
     //   public virtual System.Collections.Generic.ICollection<Game> Games { get; set; }
      //  public virtual System.Collections.Generic.ICollection<Tournament> Tournaments { get; set; }

        public AspNetUser()
        {
            AspNetUserClaims = new System.Collections.Generic.List<AspNetUserClaim>();
            AspNetUserLogins = new System.Collections.Generic.List<AspNetUserLogin>();
            AspNetUserRoles = new System.Collections.Generic.List<AspNetUserRole>();
        //    Games = new System.Collections.Generic.List<Game>();
            //Tournaments = new System.Collections.Generic.List<Tournament>();
        }
    }

}

