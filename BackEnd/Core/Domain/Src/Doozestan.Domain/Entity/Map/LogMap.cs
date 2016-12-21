
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class LogMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Log>
    {
        public LogMap()
            : this("dbo")
        {
        }

        public LogMap(string schema)
        {
            ToTable("Logs", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.EventDateTime).HasColumnName(@"EventDateTime").IsRequired().HasColumnType("datetime");
            Property(x => x.EventLevel).HasColumnName(@"EventLevel").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.UserName).HasColumnName(@"UserName").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.MachineName).HasColumnName(@"MachineName").IsRequired().HasColumnType("nvarchar").HasMaxLength(100);
            Property(x => x.EventMessage).HasColumnName(@"EventMessage").IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.ErrorSource).HasColumnName(@"ErrorSource").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.ErrorClass).HasColumnName(@"ErrorClass").IsOptional().HasColumnType("nvarchar").HasMaxLength(500);
            Property(x => x.ErrorMethod).HasColumnName(@"ErrorMethod").IsOptional().HasColumnType("nvarchar(max)");
            Property(x => x.InnerErrorMessage).HasColumnName(@"InnerErrorMessage").IsOptional().HasColumnType("nvarchar(max)");
        }
    }

}

