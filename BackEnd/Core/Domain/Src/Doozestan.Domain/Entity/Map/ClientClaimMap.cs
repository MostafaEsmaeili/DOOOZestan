
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientClaimMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientClaim>
    {
        public ClientClaimMap()
            : this("dbo")
        {
        }

        public ClientClaimMap(string schema)
        {
            ToTable("ClientClaims", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Type).HasColumnName(@"Type").IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.Value).HasColumnName(@"Value").IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientClaims).HasForeignKey(c => c.ClientId);
        }
    }

}

