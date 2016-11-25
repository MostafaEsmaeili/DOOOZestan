
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{


    public class DoozestanDbContext :Framework.DataAccess.DataContext.DataContext, IDoozestanDbContext
    {
        public System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; }
        public System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; }
        public System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public System.Data.Entity.DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public System.Data.Entity.DbSet<Game> Games { get; set; }
        public System.Data.Entity.DbSet<Tournament> Tournaments { get; set; }

        static DoozestanDbContext()
        {
            System.Data.Entity.Database.SetInitializer<DoozestanDbContext>(null);
        }

        public DoozestanDbContext()
            : base("Name=DoozestanDataContext")
        {
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserRoleMap());
            modelBuilder.Configurations.Add(new GameMap());
            modelBuilder.Configurations.Add(new TournamentMap());
        }

        public static System.Data.Entity.DbModelBuilder CreateModel(System.Data.Entity.DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap(schema));
            modelBuilder.Configurations.Add(new AspNetUserMap(schema));
            modelBuilder.Configurations.Add(new AspNetUserClaimMap(schema));
            modelBuilder.Configurations.Add(new AspNetUserLoginMap(schema));
            modelBuilder.Configurations.Add(new AspNetUserRoleMap(schema));
            modelBuilder.Configurations.Add(new GameMap(schema));
            modelBuilder.Configurations.Add(new TournamentMap(schema));
            return modelBuilder;
        }
    }
}

