
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserClaimMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUserClaim>
    {
        public AspNetUserClaimMap()
            : this("dbo")
        {
        }

        public AspNetUserClaimMap(string schema)
        {
            ToTable("AspNetUserClaims", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName(@"UserId").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ClaimType).HasColumnName(@"ClaimType").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ClaimValue).HasColumnName(@"ClaimValue").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.IdentityUserId).HasColumnName(@"IdentityUser_Id").IsOptional().HasColumnType("nvarchar").HasMaxLength(128);

            HasOptional(a => a.AspNetUser).WithMany(b => b.AspNetUserClaims).HasForeignKey(c => c.IdentityUserId).WillCascadeOnDelete(false);
        }
    }

}

