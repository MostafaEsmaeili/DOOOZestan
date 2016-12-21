
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ConsentMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Consent>
    {
        public ConsentMap()
            : this("dbo")
        {
        }

        public ConsentMap(string schema)
        {
            ToTable("Consents", schema);
            HasKey(x => new { x.Subject, x.ClientId });

            Property(x => x.Subject).HasColumnName(@"Subject").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ClientId).HasColumnName(@"ClientId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Scopes).HasColumnName(@"Scopes").IsRequired().HasColumnType("nvarchar").HasMaxLength(2000);
        }
    }

}

