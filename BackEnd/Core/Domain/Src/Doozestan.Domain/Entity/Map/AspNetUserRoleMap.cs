
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserRoleMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUserRole>
    {
        public AspNetUserRoleMap()
            : this("dbo")
        {
        }

        public AspNetUserRoleMap(string schema)
        {
            ToTable("AspNetUserRoles", schema);
            HasKey(x => new { x.UserId, x.RoleId });

            Property(x => x.UserId).HasColumnName(@"UserId").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.RoleId).HasColumnName(@"RoleId").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.IdentityUserId).HasColumnName(@"IdentityUser_Id").IsOptional().HasColumnType("nvarchar").HasMaxLength(128);

            HasOptional(a => a.AspNetUser).WithMany(b => b.AspNetUserRoles).HasForeignKey(c => c.IdentityUserId).WillCascadeOnDelete(false);
            HasRequired(a => a.AspNetRole).WithMany(b => b.AspNetUserRoles).HasForeignKey(c => c.RoleId);
        }
    }

}

