
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientPostLogoutRedirectUriMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public ClientPostLogoutRedirectUriMap()
            : this("dbo")
        {
        }

        public ClientPostLogoutRedirectUriMap(string schema)
        {
            ToTable("ClientPostLogoutRedirectUris", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Uri).HasColumnName(@"Uri").IsRequired().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientPostLogoutRedirectUris).HasForeignKey(c => c.ClientId);
        }
    }

}

