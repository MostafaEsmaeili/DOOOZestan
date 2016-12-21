
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientScopeMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientScope>
    {
        public ClientScopeMap()
            : this("dbo")
        {
        }

        public ClientScopeMap(string schema)
        {
            ToTable("ClientScopes", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Scope).HasColumnName(@"Scope").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientScopes).HasForeignKey(c => c.ClientId);
        }
    }

}

