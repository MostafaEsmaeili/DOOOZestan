
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Client>
    {
        public ClientMap()
            : this("dbo")
        {
        }

        public ClientMap(string schema)
        {
            ToTable("Clients", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).HasColumnName(@"Enabled").IsRequired().HasColumnType("bit");
            Property(x => x.ClientId).HasColumnName(@"ClientId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ClientName).HasColumnName(@"ClientName").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ClientUri).HasColumnName(@"ClientUri").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.LogoUri).HasColumnName(@"LogoUri").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.RequireConsent).HasColumnName(@"RequireConsent").IsRequired().HasColumnType("bit");
            Property(x => x.AllowRememberConsent).HasColumnName(@"AllowRememberConsent").IsRequired().HasColumnType("bit");
            Property(x => x.AllowAccessTokensViaBrowser).HasColumnName(@"AllowAccessTokensViaBrowser").IsRequired().HasColumnType("bit");
            Property(x => x.Flow).HasColumnName(@"Flow").IsRequired().HasColumnType("int");
            Property(x => x.AllowClientCredentialsOnly).HasColumnName(@"AllowClientCredentialsOnly").IsRequired().HasColumnType("bit");
            Property(x => x.LogoutUri).HasColumnName(@"LogoutUri").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.LogoutSessionRequired).HasColumnName(@"LogoutSessionRequired").IsRequired().HasColumnType("bit");
            Property(x => x.RequireSignOutPrompt).HasColumnName(@"RequireSignOutPrompt").IsRequired().HasColumnType("bit");
            Property(x => x.AllowAccessToAllScopes).HasColumnName(@"AllowAccessToAllScopes").IsRequired().HasColumnType("bit");
            Property(x => x.IdentityTokenLifetime).HasColumnName(@"IdentityTokenLifetime").IsRequired().HasColumnType("int");
            Property(x => x.AccessTokenLifetime).HasColumnName(@"AccessTokenLifetime").IsRequired().HasColumnType("int");
            Property(x => x.AuthorizationCodeLifetime).HasColumnName(@"AuthorizationCodeLifetime").IsRequired().HasColumnType("int");
            Property(x => x.AbsoluteRefreshTokenLifetime).HasColumnName(@"AbsoluteRefreshTokenLifetime").IsRequired().HasColumnType("int");
            Property(x => x.SlidingRefreshTokenLifetime).HasColumnName(@"SlidingRefreshTokenLifetime").IsRequired().HasColumnType("int");
            Property(x => x.RefreshTokenUsage).HasColumnName(@"RefreshTokenUsage").IsRequired().HasColumnType("int");
            Property(x => x.UpdateAccessTokenOnRefresh).HasColumnName(@"UpdateAccessTokenOnRefresh").IsRequired().HasColumnType("bit");
            Property(x => x.RefreshTokenExpiration).HasColumnName(@"RefreshTokenExpiration").IsRequired().HasColumnType("int");
            Property(x => x.AccessTokenType).HasColumnName(@"AccessTokenType").IsRequired().HasColumnType("int");
            Property(x => x.EnableLocalLogin).HasColumnName(@"EnableLocalLogin").IsRequired().HasColumnType("bit");
            Property(x => x.IncludeJwtId).HasColumnName(@"IncludeJwtId").IsRequired().HasColumnType("bit");
            Property(x => x.AlwaysSendClientClaims).HasColumnName(@"AlwaysSendClientClaims").IsRequired().HasColumnType("bit");
            Property(x => x.PrefixClientClaims).HasColumnName(@"PrefixClientClaims").IsRequired().HasColumnType("bit");
            Property(x => x.AllowAccessToAllGrantTypes).HasColumnName(@"AllowAccessToAllGrantTypes").IsRequired().HasColumnType("bit");
        }
    }

}

