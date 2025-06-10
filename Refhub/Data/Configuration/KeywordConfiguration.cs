using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Refhub.Data.Models;

namespace Refhub.Data.Configuration;

public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Word).IsRequired().HasMaxLength(50);

        builder.HasMany(k => k.BookKeywords).WithOne(bk => bk.Keyword)
            .HasForeignKey(bk => bk.KeywordId);
    }
}
