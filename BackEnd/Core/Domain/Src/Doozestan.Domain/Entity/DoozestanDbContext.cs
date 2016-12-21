
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    using System.Linq;

    public class DoozestanDbContext : IdentityDbContext<ApplicationUser>//, IDoozestanDbContext
    {
        //public DbSet<AspNetRole> AspNetRoles { get; set; }
        //public DbSet<AspNetUser> AspNetUsers { get; set; }
        //public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientClaim> ClientClaims { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<Consent> Consents { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<ScopeClaim> ScopeClaims { get; set; }
        public DbSet<ScopeSecret> ScopeSecrets { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        static DoozestanDbContext()
        {

            Database.SetInitializer<IdentityDbContext>(null);

            Database.SetInitializer<DoozestanDbContext>(null);

        }

        public DoozestanDbContext()
            : base("Name=DoozestanDataContext")
        {
           Database.SetInitializer<IdentityDbContext>(null);

           Database.SetInitializer<DoozestanDbContext>(null);
        }

        public bool IsSqlParameterNull(System.Data.SqlClient.SqlParameter param)
        {
            var sqlValue = param.SqlValue;
            var nullableValue = sqlValue as System.Data.SqlTypes.INullable;
            if (nullableValue != null)
                return nullableValue.IsNull;
            return (sqlValue == null || sqlValue == System.DBNull.Value);
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


            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Configurations.Add(new ClientClaimMap());
            modelBuilder.Configurations.Add(new ClientCorsOriginMap());
            modelBuilder.Configurations.Add(new ClientCustomGrantTypeMap());
            modelBuilder.Configurations.Add(new ClientIdPRestrictionMap());
            modelBuilder.Configurations.Add(new ClientPostLogoutRedirectUriMap());
            modelBuilder.Configurations.Add(new ClientRedirectUriMap());
            modelBuilder.Configurations.Add(new ClientScopeMap());
            modelBuilder.Configurations.Add(new ClientSecretMap());
            modelBuilder.Configurations.Add(new ConsentMap());
            modelBuilder.Configurations.Add(new GameMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new ScopeMap());
            modelBuilder.Configurations.Add(new ScopeClaimMap());
            modelBuilder.Configurations.Add(new ScopeSecretMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new TournamentMap());
        }

      ////  public  DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
      //  {
      //      //modelBuilder.Configurations.Add(new AspNetRoleMap(schema));
      //      //modelBuilder.Configurations.Add(new AspNetUserMap(schema));
      //      //modelBuilder.Configurations.Add(new AspNetUserClaimMap(schema));
      //      //modelBuilder.Configurations.Add(new AspNetUserLoginMap(schema));
      //      //modelBuilder.Configurations.Add(new AspNetUserRoleMap(schema));
      //      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      //           base.OnModelCreating(modelBuilder);
      //      modelBuilder.Entity<IdentityUser>()
      // .ToTable("dbo.AspNetUsers");
      //      modelBuilder.Entity<ApplicationUser>()
      //            .ToTable("dbo.AspNetUsers");

      //      modelBuilder.Entity<IdentityRole>()
      //           .ToTable("dbo.AspNetRoles");
      //      modelBuilder.Entity<ApplicationRole>()
      //           .ToTable("dbo.AspNetRoles");

      //      modelBuilder.Entity<IdentityUserClaim>().ToTable("dbo.AspNetUserClaims");

      //      modelBuilder.Entity<IdentityUserRole>().ToTable("dbo.AspNetUserRoles");
      //      modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("dbo.AspNetUserRoles");

      //      modelBuilder.Entity<IdentityUserLogin>().ToTable("dbo.AspNetUserLogins");
      //      modelBuilder.Configurations.Add(new ClientMap(schema));
      //      modelBuilder.Configurations.Add(new ClientClaimMap(schema));
      //      modelBuilder.Configurations.Add(new ClientCorsOriginMap(schema));
      //      modelBuilder.Configurations.Add(new ClientCustomGrantTypeMap(schema));
      //      modelBuilder.Configurations.Add(new ClientIdPRestrictionMap(schema));
      //      modelBuilder.Configurations.Add(new ClientPostLogoutRedirectUriMap(schema));
      //      modelBuilder.Configurations.Add(new ClientRedirectUriMap(schema));
      //      modelBuilder.Configurations.Add(new ClientScopeMap(schema));
      //      modelBuilder.Configurations.Add(new ClientSecretMap(schema));
      //      modelBuilder.Configurations.Add(new ConsentMap(schema));
      //      modelBuilder.Configurations.Add(new GameMap(schema));
      //      modelBuilder.Configurations.Add(new LogMap(schema));
      //      modelBuilder.Configurations.Add(new ScopeMap(schema));
      //      modelBuilder.Configurations.Add(new ScopeClaimMap(schema));
      //      modelBuilder.Configurations.Add(new ScopeSecretMap(schema));
      //      modelBuilder.Configurations.Add(new TokenMap(schema));
      //      modelBuilder.Configurations.Add(new TournamentMap(schema));
      //      return modelBuilder;
      //  }
        
        // Stored Procedures
        public int AddTournament(int? gameId, string userId)
        {
            var gameIdParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@GameId", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = gameId.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!gameId.HasValue)
                gameIdParam.Value = System.DBNull.Value;

            var userIdParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@UserId", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = System.Data.ParameterDirection.Input, Value = userId, Size = 128 };
            if (userIdParam.Value == null)
                userIdParam.Value = System.DBNull.Value;

            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
 
            Database.ExecuteSqlCommand("EXEC @procResult = [dbo].[AddTournament] @GameId, @UserId", gameIdParam, userIdParam, procResultParam);
 
            return (int) procResultParam.Value;
        }

        public System.Collections.Generic.List<GetAllGameReturnModel> GetAllGame()
        {
            int procResult;
            return GetAllGame(out procResult);
        }

        public System.Collections.Generic.List<GetAllGameReturnModel> GetAllGame(out int procResult)
        {
            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
            var procResultData = Database.SqlQuery<GetAllGameReturnModel>("EXEC @procResult = [dbo].[GetAllGame] ", procResultParam).ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<GetAllGameReturnModel>> GetAllGameAsync()
        {
            var procResultData = await Database.SqlQuery<GetAllGameReturnModel>("EXEC [dbo].[GetAllGame] ").ToListAsync();

            return procResultData;
        }

        public System.Collections.Generic.List<GetGameByUserIdReturnModel> GetGameByUserId(string userId)
        {
            int procResult;
            return GetGameByUserId(userId, out procResult);
        }

        public System.Collections.Generic.List<GetGameByUserIdReturnModel> GetGameByUserId(string userId, out int procResult)
        {
            var userIdParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@UserID", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = System.Data.ParameterDirection.Input, Value = userId, Size = 128 };
            if (userIdParam.Value == null)
                userIdParam.Value = System.DBNull.Value;

            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
            var procResultData = Database.SqlQuery<GetGameByUserIdReturnModel>("EXEC @procResult = [dbo].[GetGameByUserId] @UserID", userIdParam, procResultParam).ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<GetGameByUserIdReturnModel>> GetGameByUserIdAsync(string userId)
        {
            var userIdParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@UserID", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = System.Data.ParameterDirection.Input, Value = userId, Size = 128 };
            if (userIdParam.Value == null)
                userIdParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<GetGameByUserIdReturnModel>("EXEC [dbo].[GetGameByUserId] @UserID", userIdParam).ToListAsync();

            return procResultData;
        }

        public System.Collections.Generic.List<GetGamesByIdReturnModel> GetGamesById(int? id)
        {
            int procResult;
            return GetGamesById(id, out procResult);
        }

        public System.Collections.Generic.List<GetGamesByIdReturnModel> GetGamesById(int? id, out int procResult)
        {
            var idParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
            var procResultData = Database.SqlQuery<GetGamesByIdReturnModel>("EXEC @procResult = [dbo].[GetGamesById] @Id", idParam, procResultParam).ToList();

            procResult = (int) procResultParam.Value;
            return procResultData;
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<GetGamesByIdReturnModel>> GetGamesByIdAsync(int? id)
        {
            var idParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var procResultData = await Database.SqlQuery<GetGamesByIdReturnModel>("EXEC [dbo].[GetGamesById] @Id", idParam).ToListAsync();

            return procResultData;
        }

        public int UpdateGame(int? id, string winnerId, int? status, System.DateTime? endDate)
        {
            var idParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = id.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!id.HasValue)
                idParam.Value = System.DBNull.Value;

            var winnerIdParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@WinnerId", SqlDbType = System.Data.SqlDbType.NVarChar, Direction = System.Data.ParameterDirection.Input, Value = winnerId, Size = 128 };
            if (winnerIdParam.Value == null)
                winnerIdParam.Value = System.DBNull.Value;

            var statusParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Input, Value = status.GetValueOrDefault(), Precision = 10, Scale = 0 };
            if (!status.HasValue)
                statusParam.Value = System.DBNull.Value;

            var endDateParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@EndDate", SqlDbType = System.Data.SqlDbType.DateTime, Direction = System.Data.ParameterDirection.Input, Value = endDate.GetValueOrDefault() };
            if (!endDate.HasValue)
                endDateParam.Value = System.DBNull.Value;

            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = "@procResult", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };
 
            Database.ExecuteSqlCommand("EXEC @procResult = [dbo].[UpdateGame] @Id, @WinnerId, @Status, @EndDate", idParam, winnerIdParam, statusParam, endDateParam, procResultParam);
 
            return (int) procResultParam.Value;
        }

    }
}

