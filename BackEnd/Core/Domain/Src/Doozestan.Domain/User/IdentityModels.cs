using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Doozestan.Common.WcfService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Doozestan.Domain
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual string DisplayName { get; set; }
       public virtual bool IsAdmin { get; set; }
        public virtual bool IsCustomizedAccess { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int? Status { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager )
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,string auth)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, auth);
            // Add custom user claims here
            return userIdentity;
        }

    }


    public class ApplicationRole : IdentityRole
    {

    }

    public class CustomIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CustomIdentityDbContext()
            : base("Name=DoozestanDataContext", false)
        {

            Database.SetInitializer<IdentityDbContext>(null);
            Database.SetInitializer<DoozestanDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                   .ToTable("dbo.AspNetUsers");
            modelBuilder.Entity<ApplicationUser>()
                  .ToTable("dbo.AspNetUsers");

            modelBuilder.Entity<IdentityRole>()
                 .ToTable("dbo.AspNetRoles");
            modelBuilder.Entity<ApplicationRole>()
                 .ToTable("dbo.AspNetRoles");

            modelBuilder.Entity<IdentityUserClaim>().ToTable("dbo.AspNetUserClaims");

            modelBuilder.Entity<IdentityUserRole>().ToTable("dbo.AspNetUserRoles");
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("dbo.AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>().ToTable("dbo.AspNetUserLogins");

        }


    }
    public class BaseServiceResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
    }


}
