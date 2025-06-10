using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Refhub.Data.Models;

namespace Refhub.Data.Configuration;

public class BookKeywordConfiguration : IEntityTypeConfiguration<BookKeyword>
{
    public void Configure(EntityTypeBuilder<BookKeyword> builder)
    {
        builder.HasKey(bk => new { bk.BookId, bk.KeywordId });

        builder.HasOne(bk => bk.Book).WithMany(b => b.BookKeywords)
            .HasForeignKey(bk => bk.BookId);

        builder.HasOne(bk => bk.Keyword).WithMany(b => b.BookKeywords)
            .HasForeignKey(bk => bk.KeywordId);
    }
}
