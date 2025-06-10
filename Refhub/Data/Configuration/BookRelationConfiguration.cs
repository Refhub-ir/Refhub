using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Refhub_Ir.Models;

namespace Refhub_Ir.Data.Configuration
{
    public class BookRelationConfiguration : IEntityTypeConfiguration<BookRelation>
    {
        public void Configure(EntityTypeBuilder<BookRelation> builder)
        {
            builder.HasKey(rb => new { rb.BookId, rb.RelatedBookId });

            builder.HasOne(br=>br.Book).WithMany(b=>b.RelatedTo)
                .HasForeignKey(b=>b.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(br=>br.RelatedBook).WithMany(b=>b.RelatedFrom)
                .HasForeignKey(br=>br.RelatedBookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
