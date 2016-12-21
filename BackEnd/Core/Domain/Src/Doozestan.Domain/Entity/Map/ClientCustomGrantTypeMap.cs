
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientCustomGrantTypeMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientCustomGrantType>
    {
        public ClientCustomGrantTypeMap()
            : this("dbo")
        {
        }

        public ClientCustomGrantTypeMap(string schema)
        {
            ToTable("ClientCustomGrantTypes", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.GrantType).HasColumnName(@"GrantType").IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientCustomGrantTypes).HasForeignKey(c => c.ClientId);
        }
    }

}

