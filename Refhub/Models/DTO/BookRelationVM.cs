using Refhub_Ir.Models.Books;

namespace Refhub_Ir.Models.DTO
{
    public class BookRelationVM
    {
        public int BookId { get; set; }
        public BookVM Book { get; set; } = new();
        public int RelatedBookId { get; set; }
        public BookVM RelatedBook { get; set; } = new();
    }
}
