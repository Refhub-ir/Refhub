namespace Refhub_Ir.Data.Models;

public class BookRelation
{
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int RelatedBookId { get; set; }
    public Book? RelatedBook { get; set; }
}

