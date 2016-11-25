
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class AspNetUserMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AspNetUser>
    {
        public AspNetUserMap()
            : this("dbo")
        {
        }

        public AspNetUserMap(string schema)
        {
            ToTable("AspNetUsers", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.Email).HasColumnName(@"Email").IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.EmailConfirmed).HasColumnName(@"EmailConfirmed").IsRequired().HasColumnType("bit");
            Property(x => x.PasswordHash).HasColumnName(@"PasswordHash").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.SecurityStamp).HasColumnName(@"SecurityStamp").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.PhoneNumberConfirmed).HasColumnName(@"PhoneNumberConfirmed").IsRequired().HasColumnType("bit");
            Property(x => x.TwoFactorEnabled).HasColumnName(@"TwoFactorEnabled").IsRequired().HasColumnType("bit");
            Property(x => x.LockoutEndDateUtc).HasColumnName(@"LockoutEndDateUtc").IsOptional().HasColumnType("datetime");
            Property(x => x.LockoutEnabled).HasColumnName(@"LockoutEnabled").IsRequired().HasColumnType("bit");
            Property(x => x.AccessFailedCount).HasColumnName(@"AccessFailedCount").IsRequired().HasColumnType("int");
            Property(x => x.UserName).HasColumnName(@"UserName").IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.DisplayName).HasColumnName(@"DisplayName").IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.CreateDate).HasColumnName(@"CreateDate").IsOptional().HasColumnType("datetime");
            Property(x => x.Discriminator).HasColumnName(@"Discriminator").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);
            Property(x => x.IsActive).HasColumnName(@"IsActive").IsOptional().HasColumnType("bit");
        }
    }

}

