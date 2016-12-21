
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ScopeSecretMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScopeSecret>
    {
        public ScopeSecretMap()
            : this("dbo")
        {
        }

        public ScopeSecretMap(string schema)
        {
            ToTable("ScopeSecrets", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Description).HasColumnName(@"Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.Expiration).HasColumnName(@"Expiration").IsOptional().HasColumnType("datetimeoffset");
            Property(x => x.Type).HasColumnName(@"Type").IsOptional().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.Value).HasColumnName(@"Value").IsRequired().HasColumnType("nvarchar").HasMaxLength(250);
            Property(x => x.ScopeId).HasColumnName(@"Scope_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Scope).WithMany(b => b.ScopeSecrets).HasForeignKey(c => c.ScopeId);
        }
    }

}

