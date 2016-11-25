
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class TournamentMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Tournament>
    {
        public TournamentMap()
            : this("dbo")
        {
        }

        public TournamentMap(string schema)
        {
            ToTable("Tournament", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.GameId).HasColumnName(@"GameId").IsRequired().HasColumnType("int");
            Property(x => x.UserId).HasColumnName(@"UserId").IsRequired().HasColumnType("nvarchar").HasMaxLength(128);

            //HasRequired(a => a.AspNetUser).WithMany(b => b.Tournaments).HasForeignKey(c => c.UserId).WillCascadeOnDelete(false);
            //HasRequired(a => a.Game).WithMany(b => b.Tournaments).HasForeignKey(c => c.GameId).WillCascadeOnDelete(false);
        }
    }

}

