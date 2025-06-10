using Refhub.Models.Books;

namespace Refhub.Models.DTO;

public class BookRelationVM
{
    public int BookId { get; set; }
    public BookVM Book { get; set; } = new();
    public int RelatedBookId { get; set; }
    public BookVM RelatedBook { get; set; } = new();
}
