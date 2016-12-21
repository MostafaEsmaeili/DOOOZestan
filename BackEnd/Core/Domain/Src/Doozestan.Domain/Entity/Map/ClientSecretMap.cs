
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientSecretMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientSecret>
    {
        public ClientSecretMap()
            : this("dbo")
        {
        }

        public ClientSecretMap(string schema)
        {
            ToTable("ClientSecrets", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Value).HasColumnName(@"Value").IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.Type).HasColumnName(@"Type").IsOptional().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.Description).HasColumnName(@"Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(2000);
            Property(x => x.Expiration).HasColumnName(@"Expiration").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientSecrets).HasForeignKey(c => c.ClientId);
        }
    }

}

