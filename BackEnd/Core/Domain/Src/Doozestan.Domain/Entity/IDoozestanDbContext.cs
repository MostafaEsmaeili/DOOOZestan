
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public interface IDoozestanDbContext : System.IDisposable
    {
        //System.Data.Entity.DbSet<AspNetRole> AspNetRoles { get; set; }
        //System.Data.Entity.DbSet<AspNetUser> AspNetUsers { get; set; }
        //System.Data.Entity.DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //System.Data.Entity.DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //System.Data.Entity.DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        System.Data.Entity.DbSet<Client> Clients { get; set; }
        System.Data.Entity.DbSet<ClientClaim> ClientClaims { get; set; }
        System.Data.Entity.DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        System.Data.Entity.DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
        System.Data.Entity.DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
        System.Data.Entity.DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
        System.Data.Entity.DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        System.Data.Entity.DbSet<ClientScope> ClientScopes { get; set; }
        System.Data.Entity.DbSet<ClientSecret> ClientSecrets { get; set; }
        System.Data.Entity.DbSet<Consent> Consents { get; set; }
        System.Data.Entity.DbSet<Game> Games { get; set; }
        System.Data.Entity.DbSet<Log> Logs { get; set; }
        System.Data.Entity.DbSet<Scope> Scopes { get; set; }
        System.Data.Entity.DbSet<ScopeClaim> ScopeClaims { get; set; }
        System.Data.Entity.DbSet<ScopeSecret> ScopeSecrets { get; set; }
        System.Data.Entity.DbSet<Token> Tokens { get; set; }
        System.Data.Entity.DbSet<Tournament> Tournaments { get; set; }

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);

        // Stored Procedures
        int AddTournament(int? gameId, string userId);
        // AddTournamentAsync cannot be created due to having out parameters, or is relying on the procedure result (int)

        System.Collections.Generic.List<GetAllGameReturnModel> GetAllGame();
        System.Collections.Generic.List<GetAllGameReturnModel> GetAllGame(out int procResult);
        System.Threading.Tasks.Task<System.Collections.Generic.List<GetAllGameReturnModel>> GetAllGameAsync();

        System.Collections.Generic.List<GetGameByUserIdReturnModel> GetGameByUserId(string userId);
        System.Collections.Generic.List<GetGameByUserIdReturnModel> GetGameByUserId(string userId, out int procResult);
        System.Threading.Tasks.Task<System.Collections.Generic.List<GetGameByUserIdReturnModel>> GetGameByUserIdAsync(string userId);

        System.Collections.Generic.List<GetGamesByIdReturnModel> GetGamesById(int? id);
        System.Collections.Generic.List<GetGamesByIdReturnModel> GetGamesById(int? id, out int procResult);
        System.Threading.Tasks.Task<System.Collections.Generic.List<GetGamesByIdReturnModel>> GetGamesByIdAsync(int? id);

        int UpdateGame(int? id, string winnerId, int? status, System.DateTime? endDate);
        // UpdateGameAsync cannot be created due to having out parameters, or is relying on the procedure result (int)

    }

}

