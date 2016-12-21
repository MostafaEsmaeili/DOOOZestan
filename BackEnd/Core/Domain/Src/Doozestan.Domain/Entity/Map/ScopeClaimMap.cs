
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ScopeClaimMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ScopeClaim>
    {
        public ScopeClaimMap()
            : this("dbo")
        {
        }

        public ScopeClaimMap(string schema)
        {
            ToTable("ScopeClaims", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Description).HasColumnName(@"Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.AlwaysIncludeInIdToken).HasColumnName(@"AlwaysIncludeInIdToken").IsRequired().HasColumnType("bit");
            Property(x => x.ScopeId).HasColumnName(@"Scope_Id").IsRequired().HasColumnType("int");

            HasRequired(a => a.Scope).WithMany(b => b.ScopeClaims).HasForeignKey(c => c.ScopeId);
        }
    }

}

