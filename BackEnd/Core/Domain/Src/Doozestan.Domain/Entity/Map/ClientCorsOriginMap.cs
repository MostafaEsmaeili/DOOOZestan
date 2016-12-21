
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientCorsOriginMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientCorsOrigin>
    {
        public ClientCorsOriginMap()
            : this("dbo")
        {
        }

        public ClientCorsOriginMap(string schema)
        {
            ToTable("ClientCorsOrigins", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Origin).HasColumnName(@"Origin").IsRequired().HasColumnType("nvarchar").HasMaxLength(150);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientCorsOrigins).HasForeignKey(c => c.ClientId);
        }
    }

}

