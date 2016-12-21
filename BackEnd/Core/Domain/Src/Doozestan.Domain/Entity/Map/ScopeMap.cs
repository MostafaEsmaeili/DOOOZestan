
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class ScopeMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Scope>
    {
        public ScopeMap()
            : this("dbo")
        {
        }

        public ScopeMap(string schema)
        {
            ToTable("Scopes", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Enabled).HasColumnName(@"Enabled").IsRequired().HasColumnType("bit");
            Property(x => x.Name).HasColumnName(@"Name").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.DisplayName).HasColumnName(@"DisplayName").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Description).HasColumnName(@"Description").IsOptional().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.Required).HasColumnName(@"Required").IsRequired().HasColumnType("bit");
            Property(x => x.Emphasize).HasColumnName(@"Emphasize").IsRequired().HasColumnType("bit");
            Property(x => x.Type).HasColumnName(@"Type").IsRequired().HasColumnType("int");
            Property(x => x.IncludeAllClaimsForUser).HasColumnName(@"IncludeAllClaimsForUser").IsRequired().HasColumnType("bit");
            Property(x => x.ClaimsRule).HasColumnName(@"ClaimsRule").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ShowInDiscoveryDocument).HasColumnName(@"ShowInDiscoveryDocument").IsRequired().HasColumnType("bit");
            Property(x => x.AllowUnrestrictedIntrospection).HasColumnName(@"AllowUnrestrictedIntrospection").IsRequired().HasColumnType("bit");
        }
    }

}

