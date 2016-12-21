
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Doozestan.Domain
{

    public class TokenMap : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Token>
    {
        public TokenMap()
            : this("dbo")
        {
        }

        public TokenMap(string schema)
        {
            ToTable("Tokens", schema);
            HasKey(x => new { x.Key, x.TokenType });

            Property(x => x.Key).HasColumnName(@"Key").IsRequired().HasColumnType("nvarchar").HasMaxLength(128).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TokenType).HasColumnName(@"TokenType").IsRequired().HasColumnType("smallint").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.SubjectId).HasColumnName(@"SubjectId").IsOptional().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.ClientId).HasColumnName(@"ClientId").IsRequired().HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.JsonCode).HasColumnName(@"JsonCode").IsRequired().HasColumnType("nvarchar(max)");
            Property(x => x.Expiry).HasColumnName(@"Expiry").IsRequired().HasColumnType("datetimeoffset");
        }
    }

}

