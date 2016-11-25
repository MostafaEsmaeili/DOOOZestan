
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetRoleMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetRole>
    {
        public AspNetRoleMap()
            : this("dbo")
        {
        }

        public AspNetRoleMap(string schema)
        {
            ToTable("AspNetRoles", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName(@"Name").IsRequired().HasColumnType("nvarchar").HasMaxLength(256);
            Property(x => x.Discriminator).HasColumnName(@"Discriminator").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
        }
    }

}

