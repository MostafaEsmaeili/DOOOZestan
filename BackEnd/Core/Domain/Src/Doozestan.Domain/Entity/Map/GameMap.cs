
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class GameMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Game>
    {
        public GameMap()
            : this("dbo")
        {
        }

        public GameMap(string schema)
        {
            ToTable("Game", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Title).HasColumnName(@"Title").IsOptional().HasColumnType("nvarchar").HasMaxLength(300);
            Property(x => x.StartDate).HasColumnName(@"StartDate").IsOptional().HasColumnType("datetime");
            Property(x => x.EndDate).HasColumnName(@"EndDate").IsOptional().HasColumnType("datetime");
            Property(x => x.Status).HasColumnName(@"Status").IsOptional().HasColumnType("int");
            Property(x => x.WinnerId).HasColumnName(@"WinnerId").IsOptional().HasColumnType("nvarchar").HasMaxLength(128);
            HasOptional(a => a.ApplicationUser).WithMany(b => b.Games).HasForeignKey(c => c.WinnerId).WillCascadeOnDelete(false);

        }
    }

}

