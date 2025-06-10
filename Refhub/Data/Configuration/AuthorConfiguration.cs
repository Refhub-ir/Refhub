using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Refhub_Ir.Data.Models;
using Refhub_Ir.Models;

namespace Refhub_Ir.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a=>a.Id);
            builder.Property(a=>a.FullName).IsRequired().HasMaxLength(256);
            builder.Property(a => a.Slug).IsRequired();

            builder.HasMany(a => a.BookAuthors)
            .WithOne(ba => ba.Author)
            .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
