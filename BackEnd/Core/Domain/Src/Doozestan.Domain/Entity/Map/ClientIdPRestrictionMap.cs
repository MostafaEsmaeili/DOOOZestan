
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ClientIdPRestrictionMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ClientIdPRestriction>
    {
        public ClientIdPRestrictionMap()
            : this("dbo")
        {
        }

        public ClientIdPRestrictionMap(string schema)
        {
            ToTable("ClientIdPRestrictions", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Provider).HasColumnName(@"Provider").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ClientId).HasColumnName(@"Client_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Client).WithMany(b => b.ClientIdPRestrictions).HasForeignKey(c => c.ClientId);
        }
    }

}

