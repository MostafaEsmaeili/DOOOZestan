using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Doozestan.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Doozestan.UserManagement
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual string DisplayName { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual bool? IsActive { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DoozestanDataContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<IdentityDbContext>(null);
            Database.SetInitializer<DoozestanDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                   .ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationUser>()
                  .ToTable("AspNetUsers");

            modelBuilder.Entity<IdentityRole>()
                 .ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationRole>()
                 .ToTable("AspNetRoles");

            modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


}
