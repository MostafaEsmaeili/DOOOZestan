
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public interface IDoozestanDbContext : System.IDisposable
    {
        System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; }
        System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; }
        System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        System.Data.Entity.DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        System.Data.Entity.DbSet<Game> Games { get; set; }
        System.Data.Entity.DbSet<Tournament> Tournaments { get; set; }

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
    }

}

