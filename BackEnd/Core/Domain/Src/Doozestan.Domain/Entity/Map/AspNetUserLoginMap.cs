
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserLoginMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUserLogin>
    {
        public AspNetUserLoginMap()
            : this("dbo")
        {
        }

        public AspNetUserLoginMap(string schema)
        {
            ToTable("AspNetUserLogins", schema);
            HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            Property(x => x.LoginProvider).HasColumnName(@"LoginProvider").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.ProviderKey).HasColumnName(@"ProviderKey").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.UserId).HasColumnName(@"UserId").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.IdentityUserId).HasColumnName(@"IdentityUser_Id").IsOptional().HasColumnType("nvarchar").HasMaxLength(128);

            HasOptional(a => a.AspNetUser).WithMany(b => b.AspNetUserLogins).HasForeignKey(c => c.IdentityUserId).WillCascadeOnDelete(false);
        }
    }

}

